using kch_backend.Application.DTOs.Branch;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace kch_backend.Infrastructure.Services
{
    public class BranchService : IBranchService
    {
        private readonly KchDbContext _context;

        public BranchService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<BranchDto>> GetAllAsync()
        {
            return await _context.Branches
                .Select(b => new BranchDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Location = b.Location,
                    Contact = b.Contact,
                    ManagerName = b.ManagerName,
                    CreatedOn = (DateTime)b.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<BranchDto?> GetByIdAsync(int id)
        {
            var b = await _context.Branches.FindAsync(id);
            if (b == null) return null;

            return new BranchDto
            {
                Id = b.Id,
                Name = b.Name,
                Location = b.Location,
                Contact = b.Contact,
                ManagerName = b.ManagerName,
                CreatedOn = (DateTime)b.CreatedOn
            };
        }

        public async Task<BranchDto> AddAsync(BranchDto dto)
        {
            var branch = new Branch
            {
                
                Name = dto.Name,
                Location = dto.Location,
                Contact = dto.Contact,
                ManagerName = dto.ManagerName,
                CreatedOn = DateTime.UtcNow
            };
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

           // dto.Id = branch.Id;
            dto.CreatedOn = DateTime.Now;
            return dto;
        }

        public async Task<BranchDto?> UpdateAsync(int id, BranchDto dto)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return null;

            branch.Name = dto.Name;
            branch.Location = dto.Location;
            branch.Contact = dto.Contact;
            branch.ManagerName = dto.ManagerName;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}
