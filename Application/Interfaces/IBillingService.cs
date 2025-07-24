using kch_backend.Application.DTOs.Bill;

namespace kch_backend.Application.Interfaces
{
    public interface IBillingService
    {
        Task<BillDto> GenerateBillAsync(int eventId);
        Task<BillDto?> GetBillByEventIdAsync(int eventId);
    }
}
