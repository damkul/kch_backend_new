namespace webapp_boilerplate.Application.DTOs.Auth
{
    public class RefreshTokenRequest
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

