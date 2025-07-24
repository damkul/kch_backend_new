using kch_backend.Application.DTOs.Decoration;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecorationsController : ControllerBase
    {
        private readonly IDecorationService _service;

        public DecorationsController(IDecorationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DecorationDto>>> GetAll()
        {
            return Ok(await _service.GetAllDecorationsAsync());
        }

        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<EventDecorationDto>>> GetEventDecorations(int eventId)
        {
            return Ok(await _service.GetEventDecorationsAsync(eventId));
        }

        [HttpPost("assign/{eventId}")]
        public async Task<IActionResult> AssignToEvent(int eventId, [FromBody] List<EventDecorationDto> items)
        {
            if (await _service.AssignDecorationsAsync(eventId, items))
                return Ok(new { message = "Decorations assigned." });

            return BadRequest(new { message = "Failed to assign decorations." });
        }
    }
}
