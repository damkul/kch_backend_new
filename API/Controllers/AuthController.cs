using Microsoft.AspNetCore.Mvc;
using webapp_boilerplate.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using webapp_boilerplate.Application.DTOs.Auth;
using webapp_boilerplate.Application.Interfaces;

namespace webapp_boilerplate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _authService.Login(request);
            return Ok(response);
        }

        [HttpPost("validate-fingerprint")]
        public IActionResult ValidateFingerprint([FromBody] string fingerprint)
        {
            var isValid = _authService.ValidateFingerprint(fingerprint);
            return Ok(new { IsValid = isValid });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var response = _authService.RefreshToken(request);
            if (response == null)
                return Unauthorized();
            return Ok(response);
        }
    }
}
