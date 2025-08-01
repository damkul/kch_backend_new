using Microsoft.AspNetCore.Mvc;
using kch_backend.Application.DTOs.Auth;
using kch_backend.Application.Interfaces;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var success = await _userService.RegisterUserAsync(request);
            if (!success)
                return BadRequest(new { Message = "Username already exists" });

            return Ok(new { Message = "User registered successfully" });
        }

    
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
