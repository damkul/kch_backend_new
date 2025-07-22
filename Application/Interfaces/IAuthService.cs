using webapp_boilerplate.Application.DTOs.Auth;

namespace webapp_boilerplate.Application.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Login(LoginRequest request);
        bool ValidateFingerprint(string fingerprint);
        AuthResponse RefreshToken(RefreshTokenRequest request);
    }
}
