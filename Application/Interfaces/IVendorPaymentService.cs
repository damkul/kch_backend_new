using kch_backend.Application.DTOs.Vendor;

namespace kch_backend.Application.Interfaces
{
    public interface IVendorPaymentService
    {
        Task<List<VendorPaymentDto>> GetPaymentsByEventVendorAsync(int eventVendorId);
        Task<bool> AddPaymentAsync(VendorPaymentDto dto);
        Task<bool> DeletePaymentAsync(int id);
    }
}
