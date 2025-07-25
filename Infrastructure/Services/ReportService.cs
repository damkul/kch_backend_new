using kch_backend.Application.DTOs.Reports;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;

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
            try
            {
                Log.Information("Generating Event Summary Report from {From} to {To}", from, to);

                var fromParam = new MySqlParameter("@fromDate", from ?? (object)DBNull.Value);
                var toParam = new MySqlParameter("@toDate", to ?? (object)DBNull.Value);

                var result = await _context.Set<EventSummaryDto>()
                    .FromSqlRaw("CALL GetEventSummaries(@fromDate, @toDate)", fromParam, toParam)
                    .ToListAsync();

                Log.Information("Event Summary Report generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Event Summary Report");
                throw;
            }
        }

        public async Task<List<CustomerBillingDto>> GetCustomerBillingReportAsync()
        {
            try
            {
                Log.Information("Generating Customer Billing Report");

                var result = await _context.Set<CustomerBillingDto>()
                    .FromSqlRaw("CALL GetCustomerBillingReport()")
                    .ToListAsync();

                Log.Information("Customer Billing Report generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Customer Billing Report");
                throw;
            }
        }

        public async Task<List<StockRequirementDto>> GetStockRequirementSummaryAsync(int? eventId)
        {
            try
            {
                Log.Information("Generating Stock Requirement Summary for EventId: {EventId}", eventId);

                var eventIdParam = new MySqlParameter("@eventId", eventId ?? (object)DBNull.Value);

                var result = await _context.Set<StockRequirementDto>()
                    .FromSqlRaw("CALL GetStockRequirementSummary(@eventId)", eventIdParam)
                    .ToListAsync();

                Log.Information("Stock Requirement Summary generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Stock Requirement Summary for EventId: {EventId}", eventId);
                throw;
            }
        }

        public async Task<List<VendorPaymentReportDto>> GetVendorPaymentReportAsync()
        {
            try
            {
                Log.Information("Generating Vendor Payment Report");

                var result = await _context.Set<VendorPaymentReportDto>()
                    .FromSqlRaw("CALL GetVendorPaymentReport()")
                    .ToListAsync();

                Log.Information("Vendor Payment Report generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Vendor Payment Report");
                throw;
            }
        }

        public async Task<List<PaymentSummaryDto>> GetDailyPaymentReportAsync(DateTime date)
        {
            try
            {
                Log.Information("Generating Daily Payment Report for Date: {Date}", date);

                var dateParam = new MySqlParameter("@targetDate", date);

                var result = await _context.Set<PaymentSummaryDto>()
                    .FromSqlRaw("CALL GetDailyPaymentReport(@targetDate)", dateParam)
                    .ToListAsync();

                Log.Information("Daily Payment Report generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Daily Payment Report for Date: {Date}", date);
                throw;
            }
        }

        public async Task<List<MonthlyPaymentDto>> GetMonthlyPaymentReportAsync(string yearMonth)
        {
            try
            {
                Log.Information("Generating Monthly Payment Report for YearMonth: {YearMonth}", yearMonth);

                var ymParam = new MySqlParameter("@yearMonth", yearMonth);

                var result = await _context.Set<MonthlyPaymentDto>()
                    .FromSqlRaw("CALL GetMonthlyPaymentReport(@yearMonth)", ymParam)
                    .ToListAsync();

                Log.Information("Monthly Payment Report generated with {Count} records", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating Monthly Payment Report for YearMonth: {YearMonth}", yearMonth);
                throw;
            }
        }
    }
}
