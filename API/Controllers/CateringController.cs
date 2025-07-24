using kch_backend.Application.DTOs.Recipe;
using kch_backend.Application.Interfaces;
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

        [HttpPost("assign")]
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

        [HttpGet("stock/{eventId}")]
        public async Task<ActionResult<List<CateringStockDto>>> GetCateringStock(int eventId)
        {
            var result = await _service.GetStockByEventAsync(eventId);
            return Ok(result);
        }
    }
}
