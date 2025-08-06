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

                var result = _context.EventSummaries
                    .FromSqlRaw("CALL GetEventSummaries(@fromDate, @toDate)", fromParam, toParam)
                    .AsEnumerable()
                    .ToList();

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

                var result = _context.CustomerBillings
                    .FromSqlRaw("CALL GetCustomerBillingReport()")
                    .AsEnumerable()
                    .ToList();

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

                var result = _context.StockRequirements
                    .FromSqlRaw("CALL GetStockRequirementSummary(@eventId)", eventIdParam)
                    .AsEnumerable()
                    .ToList();

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

                var result = _context.VendorPaymentReports
                    .FromSqlRaw("CALL GetVendorPaymentReport()")
                    .AsEnumerable()
                    .ToList();

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

                var result = _context.DailyPayments
                    .FromSqlRaw("CALL GetDailyPaymentReport(@targetDate)", dateParam)
                    .AsEnumerable()
                    .ToList();

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

                var result = _context.MonthlyPayments
                    .FromSqlRaw("CALL GetMonthlyPaymentReport(@yearMonth)", ymParam)
                    .AsEnumerable()
                    .ToList();

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
