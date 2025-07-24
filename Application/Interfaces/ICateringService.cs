using kch_backend.Application.DTOs.Recipe;

namespace kch_backend.Application.Interfaces
{
    public interface ICateringService
    {
        Task<bool> AssignCateringAsync(EventCateringDto dto);
        Task<List<CateringStockDto>> GetStockByEventAsync(int eventId);
    }
}
