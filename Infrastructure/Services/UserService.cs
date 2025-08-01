using kch_backend.Application.DTOs.Auth;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace kch_backend.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly KchDbContext _context;

        public UserService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserRequest request)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                {
                    Log.Warning("Registration failed - Username already exists: {Username}", request.Username);
                    return false;
                }

                var passwordHash = HashPassword(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    FullName = request.FullName,
                    Email = request.Email,
                    Role = request.Role,
                    BranchId = (int)request.BranchId
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                Log.Information("User {Username} registered successfully", request.Username);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error registering user {Username}", request.Username);
                throw;
            }
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    BranchId = u.BranchId,
                    CreatedOn = (DateTime)u.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<User> GetUserEntityByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
