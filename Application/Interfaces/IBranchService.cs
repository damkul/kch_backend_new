using kch_backend.Application.DTOs.Branch;

namespace kch_backend.Application.Interfaces
{
    public interface IBranchService
    {
        Task<List<BranchDto>> GetAllAsync();
        Task<BranchDto?> GetByIdAsync(int id);
        Task<BranchDto> AddAsync(BranchDto dto);
        Task<BranchDto?> UpdateAsync(int id, BranchDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
