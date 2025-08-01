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
      

        [HttpPost("getAllPayments")]
        public async Task<IActionResult> GetAllPayments([FromBody] VendorPaymentFilterRequest filter)
        {
            var payments = await _service.GetAllPaymentsAsync(filter.EventId);
            return Ok(payments);
        }


        // GET: api/vendor-payments/event-vendor/5
        [HttpGet("getPaymentByEvent/{eventVendorId}")]
        public async Task<ActionResult<List<VendorPaymentDto>>> GetPaymentsByEventVendor(int eventVendorId)
        {
            var result = await _service.GetPaymentsByEventVendorAsync(eventVendorId);
            return Ok(result);
        }

        // POST: api/vendor-payments
        [HttpPost("addVendorPayment")]
        public async Task<IActionResult> AddPayment([FromBody] VendorPaymentDto dto)
        {
            var result = await _service.AddPaymentAsync(dto);
            return result ? Ok(new { message = "Payment added." }) : BadRequest(new { message = "Failed to add payment." });
        }

        // DELETE: api/vendor-payments/5
        [HttpDelete("deleteVendor/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePaymentAsync(id);
            return result ? Ok(new { message = "Payment deleted." }) : NotFound();
        }

        [HttpPut("updatePayment")]
        public async Task<IActionResult> UpdatePayment([FromBody] VendorPaymentUpdateRequest request)
        {
            var result = await _service.UpdatePaymentAsync(request);
            if (!result)
                return NotFound(new { message = "Vendor payment not found or event mismatch." });

            return Ok(new { message = "Vendor payment updated successfully." });
        }

    }
}
