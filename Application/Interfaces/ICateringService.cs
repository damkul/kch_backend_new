using kch_backend.Application.DTOs.Catering;
using kch_backend.Application.DTOs.Recipe;

namespace kch_backend.Application.Interfaces
{
    public interface ICateringService
    {
        Task<bool> AssignCateringAsync(EventCateringDto dto);
        Task<List<CateringStockDto>> GetStockByEventAsync(int eventId);

        Task<List<SelectedMenuItemDto>> GetSelectedMenuByEventAsync(int eventId);
    }
}
