using kch_backend.Application.DTOs.Auth;
using kch_backend.Entities;

namespace kch_backend.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserRequest request);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<User> GetUserEntityByUsernameAsync(string username);
    }
}
