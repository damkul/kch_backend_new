using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
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

        [HttpGet("categories")]
        public async Task<ActionResult<List<VendorCategoryDto>>> GetCategories()
        {
            return Ok(await _service.GetCategoriesAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<VendorDto>>> GetAll()
        {
            return Ok(await _service.GetAllVendorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendorDto?>> Get(int id)
        {
            var vendor = await _service.GetVendorByIdAsync(id);
            return vendor == null ? NotFound() : Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] VendorDto dto)
        {
            var result = await _service.AddOrUpdateVendorAsync(dto);
            return result ? Ok(new { message = "Saved successfully" }) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteVendorAsync(id);
            return result ? Ok(new { message = "Deleted successfully" }) : NotFound();
        }
    }
}
