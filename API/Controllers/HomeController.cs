using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("event-stats")]
        public async Task<IActionResult> GetEventStats()
        {
            var result = await _homeService.GetEventStatsForMonthAsync();
            return Ok(result);
        }
    }
}
