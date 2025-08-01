using kch_backend.Application.DTOs.Event;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly KchDbContext _context;

        public FacilityService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<FacilityDto>> GetAllAsync()
        {
            try
            {
                Log.Information("Fetching all facilities");
                var facilities = await _context.Facilities
                    .Select(f => new FacilityDto
                    {
                        Id = f.Id,
                        Name = f.Name ?? string.Empty,
                        Description = f.Included.HasValue && f.Included.Value ? "Included" : "Extra",
                        IsDefault = f.Included ?? false,
                        Rate = f.ExtraCharge ?? 0
                    })
                    .ToListAsync();

                return facilities;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching facilities");
                throw;
            }
        }

        public async Task<FacilityDto?> GetByIdAsync(int id)
        {
            try
            {
                Log.Information("Fetching facility with ID: {Id}", id);
                var f = await _context.Facilities.FindAsync(id);
                if (f == null) return null;

                return new FacilityDto
                {
                    Id = f.Id,
                    Name = f.Name ?? string.Empty,
                    Description = f.Included.HasValue && f.Included.Value ? "Included" : "Extra",
                    IsDefault = f.Included ?? false,
                    Rate = f.ExtraCharge ?? 0
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching facility with ID: {Id}", id);
                throw;
            }
        }

        public async Task<FacilityDto> AddAsync(FacilityDto dto)
        {
            try
            {
                Log.Information("Adding new facility: {Name}", dto.Name);
                var facility = new Facility
                {
                    Name = dto.Name,
                    Included = dto.IsDefault,
                    ExtraCharge = dto.Rate
                };

                _context.Facilities.Add(facility);
                await _context.SaveChangesAsync();

                dto.Id = facility.Id;
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding facility");
                throw;
            }
        }

        public async Task<FacilityDto?> UpdateAsync(int id, FacilityDto dto)
        {
            try
            {
                Log.Information("Updating facility with ID: {Id}", id);
                var f = await _context.Facilities.FindAsync(id);
                if (f == null) return null;

                f.Name = dto.Name;
                f.Included = dto.IsDefault;
                f.ExtraCharge = dto.Rate;

                await _context.SaveChangesAsync();
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating facility with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Log.Information("Deleting facility with ID: {Id}", id);
                var f = await _context.Facilities.FindAsync(id);
                if (f == null) return false;

                _context.Facilities.Remove(f);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting facility with ID: {Id}", id);
                throw;
            }
        }
    }
}
