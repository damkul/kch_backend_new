namespace kch_backend.Application.DTOs.Auth
{
   
        public class AuthResponse
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string Fingerprint { get; set; }
        }
    
}
