using kch_backend.Application.DTOs.Customer;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace kch_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("getCustomerById/{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("addCustomer")]
        public async Task<ActionResult> Create([FromBody] JsonElement requestBody)
        {
            if (requestBody.ValueKind == JsonValueKind.Array)
            {
                var customers = JsonSerializer.Deserialize<List<CustomerDto>>(requestBody.ToString());
                var created = await _service.AddAsync(customers);
                return Ok(created);
            }
            else if (requestBody.ValueKind == JsonValueKind.Object)
            {
                var customer = JsonSerializer.Deserialize<CustomerDto>(requestBody.ToString());
                var created = await _service.AddAsync(customer);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            return BadRequest("Invalid request format.");
        }

        [HttpPut("updateCustomer")]
        public async Task<ActionResult> Update([FromBody] JsonElement requestBody)
        {
            if (requestBody.ValueKind == JsonValueKind.Array)
            {
                var customers = JsonSerializer.Deserialize<List<CustomerDto>>(requestBody.ToString());
                var updated = await _service.UpdateAsync(customers);
                return Ok(updated);
            }
            else if (requestBody.ValueKind == JsonValueKind.Object)
            {
                var customer = JsonSerializer.Deserialize<CustomerDto>(requestBody.ToString());
                var updated = await _service.UpdateAsync(customer.Id, customer);
                return updated == null ? NotFound() : Ok(updated);
            }

            return BadRequest("Invalid request format.");
        }

        [HttpDelete("delete/customer{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
