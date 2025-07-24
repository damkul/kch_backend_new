using kch_backend.Application.DTOs.Event;

namespace kch_backend.Application.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(int id);
        Task<EventDto> CreateAsync(CreateEventRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
