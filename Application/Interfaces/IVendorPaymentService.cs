using kch_backend.Application.DTOs.Vendor;

namespace kch_backend.Application.Interfaces
{
    public interface IVendorPaymentService
    {
        Task<List<VendorPaymentDto>> GetPaymentsByEventVendorAsync(int eventVendorId);
        Task<bool> AddPaymentAsync(VendorPaymentDto dto);
        Task<bool> DeletePaymentAsync(int id);
        Task<List<VendorPaymentDto>> GetAllPaymentsAsync(int? eventId);
        Task<bool> UpdatePaymentAsync(VendorPaymentUpdateRequest request);

    }
}
