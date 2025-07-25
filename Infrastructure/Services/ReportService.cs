using kch_backend.Application.DTOs.Reports;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace kch_backend.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly KchDbContext _context;

        public ReportService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventSummaryDto>> GetEventSummariesAsync(DateTime? from, DateTime? to)
        {
            var fromParam = new MySqlParameter("@fromDate", from ?? (object)DBNull.Value);
            var toParam = new MySqlParameter("@toDate", to ?? (object)DBNull.Value);

            return await _context.Set<EventSummaryDto>()
                .FromSqlRaw("CALL GetEventSummaries(@fromDate, @toDate)", fromParam, toParam)
                .ToListAsync();
        }

        public async Task<List<CustomerBillingDto>> GetCustomerBillingReportAsync()
        {
            return await _context.Set<CustomerBillingDto>()
                .FromSqlRaw("CALL GetCustomerBillingReport()")
                .ToListAsync();
        }

        public async Task<List<StockRequirementDto>> GetStockRequirementSummaryAsync(int? eventId)
        {
            var eventIdParam = new MySqlParameter("@eventId", eventId ?? (object)DBNull.Value);

            return await _context.Set<StockRequirementDto>()
                .FromSqlRaw("CALL GetStockRequirementSummary(@eventId)", eventIdParam)
                .ToListAsync();
        }

        public async Task<List<VendorPaymentReportDto>> GetVendorPaymentReportAsync()
        {
            return await _context.Set<VendorPaymentReportDto>()
                .FromSqlRaw("CALL GetVendorPaymentReport()")
                .ToListAsync();
        }

        public async Task<List<PaymentSummaryDto>> GetDailyPaymentReportAsync(DateTime date)
        {
            var dateParam = new MySqlParameter("@targetDate", date);

            return await _context.Set<PaymentSummaryDto>()
                .FromSqlRaw("CALL GetDailyPaymentReport(@targetDate)", dateParam)
                .ToListAsync();
        }

        public async Task<List<MonthlyPaymentDto>> GetMonthlyPaymentReportAsync(string yearMonth)
        {
            var ymParam = new MySqlParameter("@yearMonth", yearMonth);

            return await _context.Set<MonthlyPaymentDto>()
                .FromSqlRaw("CALL GetMonthlyPaymentReport(@yearMonth)", ymParam)
                .ToListAsync();
        }

    }
}
