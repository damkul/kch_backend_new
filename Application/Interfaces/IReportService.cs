using kch_backend.Application.DTOs.Reports;

namespace kch_backend.Application.Interfaces
{
    public interface IReportService
    {
        Task<List<EventSummaryDto>> GetEventSummariesAsync(DateTime? from, DateTime? to);
        Task<List<CustomerBillingDto>> GetCustomerBillingReportAsync();
        Task<List<StockRequirementDto>> GetStockRequirementSummaryAsync(int? eventId);
        Task<List<VendorPaymentReportDto>> GetVendorPaymentReportAsync();
        Task<List<PaymentSummaryDto>> GetDailyPaymentReportAsync(DateTime date);
        Task<List<MonthlyPaymentDto>> GetMonthlyPaymentReportAsync(string yearMonth);
    }
}
