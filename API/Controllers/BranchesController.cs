using kch_backend.Application.DTOs.Branch;
using kch_backend.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace kch_backend.API.Controllers
{
    
   [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchesController(IBranchService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetById(int id)
        {
            var branch = await _service.GetByIdAsync(id);
            return branch == null ? NotFound() : Ok(branch);
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto>> Create(BranchDto dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchDto>> Update(int id, BranchDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
