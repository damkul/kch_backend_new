using kch_backend.Shared.Utils;
using System.Security.Claims;
using System.Text;
using kch_backend.Application.DTOs.Auth;
using kch_backend.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(IConfiguration config, IHttpContextAccessor httpContext)
        {
            _config = config;
            _httpContext = httpContext;
        }

        public AuthResponse Login(LoginRequest request)
        {
            try
            {
                Log.Information("Attempting login for user: {LoginId}", request.Username);

                // validate user credentials (mocked for now)
                var userId = "123"; // replace with actual DB check
                var role = "Admin";

                var userAgent = _httpContext.HttpContext?.Request.Headers["User-Agent"].ToString();
                var ip = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString();
                var fingerprint = FingerprintHelper.GenerateDeviceFingerprint(userAgent, ip);

                var token = JwtTokenHelper.GenerateJwtToken(userId, role, _config);

                Log.Information("Login successful for user: {LoginId}", request.Username);

                return new AuthResponse
                {
                    Token = token,
                    Fingerprint = fingerprint
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during login for user: {LoginId}", request.Username);
                throw;
            }
        }

        public bool ValidateFingerprint(string fingerprint)
        {
            try
            {
                var userAgent = _httpContext.HttpContext?.Request.Headers["User-Agent"].ToString();
                var ip = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString();

                var isValid = FingerprintHelper.ValidateFingerprint(fingerprint, userAgent, ip);

                Log.Information("Fingerprint validation result: {Result}", isValid);
                return isValid;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error validating fingerprint.");
                return false;
            }
        }

        public AuthResponse RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                Log.Information("Attempting to refresh token.");

                var principal = GetPrincipalFromExpiredToken(request.ExpiredToken);
                if (principal == null)
                {
                    Log.Warning("Expired token could not be validated.");
                    return null;
                }

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = principal.FindFirst(ClaimTypes.Role)?.Value;

                var newJwt = JwtTokenHelper.GenerateJwtToken(userId, role, _config);
                var newRefreshToken = Guid.NewGuid().ToString();

                var userAgent = _httpContext.HttpContext?.Request.Headers["User-Agent"].ToString();
                var ip = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString();
                var fingerprint = FingerprintHelper.GenerateDeviceFingerprint(userAgent, ip);

                Log.Information("Token refreshed successfully for userId: {UserId}", userId);

                return new AuthResponse
                {
                    Token = newJwt,
                    RefreshToken = newRefreshToken,
                    Fingerprint = fingerprint
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during token refresh.");
                throw;
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    Log.Warning("Invalid JWT signature or algorithm.");
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to parse expired token.");
                return null;
            }
        }
    }
}
