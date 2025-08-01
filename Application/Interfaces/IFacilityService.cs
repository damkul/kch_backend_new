using kch_backend.Application.DTOs.Event;

namespace kch_backend.Application.Interfaces
{
    public interface IFacilityService
    {
        Task<List<FacilityDto>> GetAllAsync();
        Task<FacilityDto?> GetByIdAsync(int id);
        Task<FacilityDto> AddAsync(FacilityDto dto);
        Task<FacilityDto?> UpdateAsync(int id, FacilityDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
