using kch_backend.Application.DTOs.Decoration;

namespace kch_backend.Application.Interfaces
{
    public interface IDecorationService
    {
        Task<List<DecorationDto>> GetAllDecorationsAsync();
        Task<List<EventDecorationDto>> GetEventDecorationsAsync(int eventId);
        Task<bool> AssignDecorationsAsync(int eventId, List<EventDecorationDto> decorations);
    }
}
