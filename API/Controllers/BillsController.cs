using kch_backend.Application.DTOs.Bill;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillsController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        // POST: api/bills/generate/5
        [HttpPost("generate/{eventId}")]
        public async Task<ActionResult<BillDto>> GenerateBill(int eventId)
        {
            try
            {
                var bill = await _billingService.GenerateBillAsync(eventId);
                return Ok(bill);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/bills/event/5
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<BillDto>> GetBillByEvent(int eventId)
        {
            var bill = await _billingService.GetBillByEventIdAsync(eventId);
            if (bill == null)
                return NotFound(new { message = "No bill found for this event." });

            return Ok(bill);
        }
    }
}
