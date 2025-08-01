namespace kch_backend.Application.DTOs.Auth
{
    public class RegisterUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int? BranchId { get; set; }
    }
}
