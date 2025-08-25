using kch_backend.Application.DTOs.Decoration;
using kch_backend.Application.DTOs.Event;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // ================= Events =================

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null)
                return NotFound(new { Message = "Event not found" });

            return Ok(ev);
        }

        [HttpPost("addEvent")]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
        {
            var createdEvent = await _eventService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpDelete("deleteEvent/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Event not found" });

            return Ok(new { Message = "Event deleted successfully" });
        }

        // ================= Facilities =================

        // POST: api/event/addFacility/101
        [HttpPost("addFacility/{eventId:int}")]
        public async Task<IActionResult> AddFacility(int eventId, [FromBody] EventFacilityDto dto)
        {
            var added = await _eventService.AddFacilityAsync(eventId, dto);
            return Ok(added);
        }

        // GET: api/event/getFacilities/101
        [HttpGet("getFacilities/{eventId:int}")]
        public async Task<IActionResult> GetFacilities(int eventId)
        {
            var list = await _eventService.GetFacilitiesByEventAsync(eventId);
            return Ok(list);
        }

        // DELETE: api/event/deleteFacility/55
        [HttpDelete("deleteFacility/{id:int}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var ok = await _eventService.DeleteFacilityAsync(id);
            if (!ok)
                return NotFound(new { Message = "Facility row not found" });

            return Ok(new { Message = "Facility removed" });
        }

        // ================= Decorations =================

        // POST: api/event/addDecoration/101
        [HttpPost("addDecoration/{eventId:int}")]
        public async Task<IActionResult> AddDecoration(int eventId, [FromBody] EventDecorationDto dto)
        {
            var added = await _eventService.AddDecorationAsync(eventId, dto);
            return Ok(added);
        }

        // GET: api/event/getDecorations/101
        [HttpGet("getDecorations/{eventId:int}")]
        public async Task<IActionResult> GetDecorations(int eventId)
        {
            var list = await _eventService.GetDecorationsByEventAsync(eventId);
            return Ok(list);
        }

        // DELETE: api/event/deleteDecoration/77
        [HttpDelete("deleteDecoration/{id:int}")]
        public async Task<IActionResult> DeleteDecoration(int id)
        {
            var ok = await _eventService.DeleteDecorationAsync(id);
            if (!ok)
                return NotFound(new { Message = "Decoration row not found" });

            return Ok(new { Message = "Decoration removed" });
        }
    }
}
