using kch_backend.Application.DTOs.Reports;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // 1. Event Summary
        [HttpGet("event-summary")]
        public async Task<ActionResult<List<EventSummaryDto>>> GetEventSummaries(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await _reportService.GetEventSummariesAsync(from, to);
            return Ok(result);
        }

        // 2. Customer Billing
        [HttpGet("customer-billing")]
        public async Task<ActionResult<List<CustomerBillingDto>>> GetCustomerBilling()
        {
            var result = await _reportService.GetCustomerBillingReportAsync();
            return Ok(result);
        }

        // 3. Stock Requirement Summary
        [HttpGet("stock-summary/{eventId?}")]
        public async Task<ActionResult<List<StockRequirementDto>>> GetStockSummary(int? eventId = null)
        {
            var result = await _reportService.GetStockRequirementSummaryAsync(eventId);
            return Ok(result);
        }

        // 4. Vendor Payment Report
        [HttpGet("vendor-payments")]
        public async Task<ActionResult<List<VendorPaymentReportDto>>> GetVendorPayments()
        {
            var result = await _reportService.GetVendorPaymentReportAsync();
            return Ok(result);
        }

        // 5. Daily Payment Report
        [HttpGet("daily-payments")]
        public async Task<ActionResult<List<PaymentSummaryDto>>> GetDailyPayments([FromQuery] DateTime date)
        {
            var result = await _reportService.GetDailyPaymentReportAsync(date);
            return Ok(result);
        }

        // 6. Monthly Payment Report
        [HttpGet("monthly-payments")]
        public async Task<ActionResult<List<MonthlyPaymentDto>>> GetMonthlyPayments([FromQuery] string yearMonth)
        {
            var result = await _reportService.GetMonthlyPaymentReportAsync(yearMonth);
            return Ok(result);
        }
    }
}
