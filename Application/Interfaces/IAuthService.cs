using kch_backend.Application.DTOs.Auth;

namespace kch_backend.Application.Interfaces
{
    public interface IAuthService
    {
        AuthResponse Login(LoginRequest request);
        bool ValidateFingerprint(string fingerprint);
        AuthResponse RefreshToken(RefreshTokenRequest request);
    }
}
