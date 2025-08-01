using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using kch_backend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _service;

        public VendorsController(IVendorService service)
        {
            _service = service;
        }

        [HttpGet("getAllVendorCategories")]
        public async Task<ActionResult<List<VendorCategoryDto>>> GetCategories()
        {
            return Ok(await _service.GetCategoriesAsync());
        }

        [HttpGet("getAllVendors")]
        public async Task<ActionResult<List<VendorDto>>> GetAll()
        {
            return Ok(await _service.GetAllVendorsAsync());
        }

        [HttpGet("getVendorById/{id}")]
        public async Task<ActionResult<VendorDto?>> Get(int id)
        {
            var vendor = await _service.GetVendorByIdAsync(id);
            return vendor == null ? NotFound() : Ok(vendor);
        }

        [HttpPost("addVendor")]
        public async Task<IActionResult> Save([FromBody] VendorDto dto)
        {
            var result = await _service.AddOrUpdateVendorAsync(dto);
            return result ? Ok(new { message = "Saved successfully" }) : BadRequest();
        }

        // Update existing vendor
        [HttpPut("updateVendor/{id}")]
        public async Task<IActionResult> Update(int id, VendorDto dto)
        {
            var success = await _service.UpdateVendorAsync(id, dto);
            if (!success)
                return NotFound(new { Message = "Vendor not found" });

            return Ok(new { Message = "Vendor updated successfully" });
        }

        [HttpDelete("deleteVendor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteVendorAsync(id);
            return result ? Ok(new { message = "Deleted successfully" }) : NotFound();
        }
    }
}
