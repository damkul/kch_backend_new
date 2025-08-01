using kch_backend.Application.DTOs.Recipe;
using kch_backend.Application.Interfaces;
using kch_backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CateringController : ControllerBase
    {
        private readonly ICateringService _service;

        public CateringController(ICateringService service)
        {
            _service = service;
        }

        [HttpPost("addCatering")]
        public async Task<IActionResult> AssignCatering([FromBody] EventCateringDto dto)
        {
            try
            {
                var result = await _service.AssignCateringAsync(dto);
                return result ? Ok(new { message = "Catering assigned and stock calculated." }) :
                                BadRequest(new { message = "Assignment failed." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getCateringStock/{eventId}")]
        public async Task<ActionResult<List<CateringStockDto>>> GetCateringStock(int eventId)
        {
            var result = await _service.GetStockByEventAsync(eventId);
            return Ok(result);
        }

        [HttpGet("event-detailed-menu-grouped/{eventId}")]
        public async Task<IActionResult> GetGroupedDetailedMenuForEvent(int eventId)
        {
            var result = await _service.GetGroupedDetailedMenuForEventAsync(eventId);
            if (result == null || result.Count == 0)
                return NotFound(new { Message = "No detailed menu found for this event" });

            return Ok(result);
        }

    }
}
