using kch_backend.Application.DTOs.Branch;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            try
            {
                Log.Information("Fetching all branches");
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching branches");
                throw;
            }
        }

        public async Task<BranchDto?> GetByIdAsync(int id)
        {
            try
            {
                Log.Information("Fetching branch with ID {Id}", id);
                var b = await _context.Branches.FindAsync(id);
                if (b == null)
                {
                    Log.Warning("Branch not found with ID {Id}", id);
                    return null;
                }

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
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching branch with ID {Id}", id);
                throw;
            }
        }

        public async Task<BranchDto> AddAsync(BranchDto dto)
        {
            try
            {
                Log.Information("Adding new branch: {Name}", dto.Name);

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

                dto.Id = branch.Id;
                dto.CreatedOn = branch.CreatedOn ?? DateTime.UtcNow;

                Log.Information("Branch added successfully with ID {Id}", branch.Id);
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while adding a new branch");
                throw;
            }
        }

        public async Task<BranchDto?> UpdateAsync(int id, BranchDto dto)
        {
            try
            {
                Log.Information("Updating branch with ID {Id}", id);

                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                {
                    Log.Warning("Branch not found with ID {Id} for update", id);
                    return null;
                }

                branch.Name = dto.Name;
                branch.Location = dto.Location;
                branch.Contact = dto.Contact;
                branch.ManagerName = dto.ManagerName;

                await _context.SaveChangesAsync();

                Log.Information("Branch updated successfully with ID {Id}", id);
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while updating branch with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Log.Information("Deleting branch with ID {Id}", id);

                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                {
                    Log.Warning("Branch not found with ID {Id} for deletion", id);
                    return false;
                }

                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();

                Log.Information("Branch deleted with ID {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting branch with ID {Id}", id);
                throw;
            }
        }
    }
}
