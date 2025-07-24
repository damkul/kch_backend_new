namespace kch_backend.Application.DTOs.Auth
{
    public class RefreshTokenRequest
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

