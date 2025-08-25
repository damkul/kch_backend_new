using kch_backend.Application.DTOs.Decoration;
using kch_backend.Application.DTOs.Event;

namespace kch_backend.Application.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(int id);
        Task<EventDto> CreateAsync(CreateEventRequest request);
        Task<bool> DeleteAsync(int id);

        // Facilities
        Task<EventFacilityDto> AddFacilityAsync(int eventId, EventFacilityDto dto);
        Task<List<EventFacilityDto>> GetFacilitiesByEventAsync(int eventId);
        Task<bool> DeleteFacilityAsync(int id);

        // Decorations
        Task<EventDecorationDto> AddDecorationAsync(int eventId, EventDecorationDto dto);
        Task<List<EventDecorationDto>> GetDecorationsByEventAsync(int eventId);
        Task<bool> DeleteDecorationAsync(int id);
    }
}
