using kch_backend.Application.Interfaces;
using kch_backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;

        public TimelineController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        [HttpGet("getEventByTImeline")]
        public async Task<ActionResult<List<TimelineEventDto>>> GetEventsByTimeline()
        {
            var result = await _timelineService.GetEventTimelineAsync();
            return Ok(result);
        }
    }
}
