using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorPaymentsController : ControllerBase
    {
        private readonly IVendorPaymentService _service;

        public VendorPaymentsController(IVendorPaymentService service)
        {
            _service = service;
        }

        // GET: api/vendor-payments/event-vendor/5
        [HttpGet("event-vendor/{eventVendorId}")]
        public async Task<ActionResult<List<VendorPaymentDto>>> GetPaymentsByEventVendor(int eventVendorId)
        {
            var result = await _service.GetPaymentsByEventVendorAsync(eventVendorId);
            return Ok(result);
        }

        // POST: api/vendor-payments
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] VendorPaymentDto dto)
        {
            var result = await _service.AddPaymentAsync(dto);
            return result ? Ok(new { message = "Payment added." }) : BadRequest(new { message = "Failed to add payment." });
        }

        // DELETE: api/vendor-payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePaymentAsync(id);
            return result ? Ok(new { message = "Payment deleted." }) : NotFound();
        }
    }
}
