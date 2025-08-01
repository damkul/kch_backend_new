using kch_backend.Infrastructure.Services;

namespace kch_backend.Application.Interfaces
{
    public interface ITimelineService
    {
        Task<List<TimelineEventDto>> GetEventTimelineAsync();
    }
}
