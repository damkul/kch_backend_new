using kch_backend.Shared.Utils;
using System.Security.Claims;
using System.Text;
using kch_backend.Application.DTOs.Auth;
using kch_backend.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace kch_backend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        /* private readonly IConfiguration _config;
         private readonly IHttpContextAccessor _httpContext;

         public AuthService(IConfiguration config, IHttpContextAccessor httpContext)
         {
             _config = config;
             _httpContext = httpContext;
         }

         public AuthResponse Login(LoginRequest request)
         {
             // validate user credentials (mocked for now)
             var userId = "123"; // fetched from DB
             var role = "Admin";

             // Generate fingerprint
             var userAgent = _httpContext.HttpContext.Request.Headers["User-Agent"].ToString();
             var ip = _httpContext.HttpContext.Connection.RemoteIpAddress?.ToString();
             var fingerprint = FingerprintHelper.GenerateDeviceFingerprint(userAgent, ip);

             // generate token
             var token = JwtTokenHelper.GenerateJwtToken(userId, role, _config);

             return new AuthResponse
             {
                 Token = token,
                 Fingerprint = fingerprint
             };
         }

         public bool ValidateFingerprint(string fingerprint)
         {
             var userAgent = _httpContext.HttpContext.Request.Headers["User-Agent"].ToString();
             var ip = _httpContext.HttpContext.Connection.RemoteIpAddress?.ToString();
             return FingerprintHelper.ValidateFingerprint(fingerprint, userAgent, ip);
         }

         public AuthResponse RefreshToken(RefreshTokenRequest request)
         {
             // For demo: you should validate refresh token against database/storage
             var principal = GetPrincipalFromExpiredToken(request.ExpiredToken);
             if (principal == null) return null;

             var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             var role = principal.FindFirst(ClaimTypes.Role)?.Value;

             var newJwt = JwtTokenHelper.GenerateJwtToken(userId, role, _config);
             var newRefreshToken = Guid.NewGuid().ToString(); // store this securely

             return new AuthResponse
             {
                 Token = newJwt,
                 RefreshToken = newRefreshToken,
                 Fingerprint = FingerprintHelper.GenerateDeviceFingerprint(
                     _httpContext.HttpContext.Request.Headers["User-Agent"],
                     _httpContext.HttpContext.Connection.RemoteIpAddress?.ToString())
             };
         }

         private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
         {
             var tokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false,
                 ValidateIssuer = false,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                 ValidateLifetime = false // important for expired token
             };

             var tokenHandler = new JwtSecurityTokenHandler();
             var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
             if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                 !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                 return null;

             return principal;
         }*/
        public AuthResponse Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public AuthResponse RefreshToken(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public bool ValidateFingerprint(string fingerprint)
        {
            throw new NotImplementedException();
        }
    }
}
