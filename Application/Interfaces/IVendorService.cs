using kch_backend.Application.DTOs.Vendor;

namespace kch_backend.Application.Interfaces
{
    public interface IVendorService
    {
        Task<List<VendorCategoryDto>> GetCategoriesAsync();
        Task<List<VendorDto>> GetAllVendorsAsync();
        Task<VendorDto?> GetVendorByIdAsync(int id);
        Task<bool> AddOrUpdateVendorAsync(VendorDto dto);
        Task<bool> DeleteVendorAsync(int id);
    }
}
