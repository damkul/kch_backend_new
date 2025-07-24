using kch_backend.Application.DTOs.Event;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace kch_backend.Infrastructure.Services
{
    public class EventService 
    {
        /*private readonly KchDbContext _context;

        public EventService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            return await _context.events
                .Include(e => e.eventfacilities)
                .ThenInclude(ef => ef.Facility)
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    CustomerId = e.CustomerId,
                    BranchId = e.BranchId,
                    EventName = e.EventName,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Notes = e.Notes,
                    CreatedOn = (DateTime)e.CreatedOn,
                    Facilities = e.eventfacilities.Select(f => new EventFacilityDto
                    {
                        Id = f.Id,
                        FacilityId = (int)f.FacilityId,
                        FacilityName = f.Facility.Name,
                        Quantity = (int)f.Quantity,
                        Rate = (int)f.Rate
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<EventDto?> GetByIdAsync(int id)
        {
            var e = await _context.events
                .Include(ev => ev.eventfacilities)
                .ThenInclude(ef => ef.Facility)
                .FirstOrDefaultAsync(ev => ev.Id == id);

            if (e == null) return null;

            return new EventDto
            {
                Id = e.Id,
                CustomerId = e.CustomerId,
                BranchId = e.BranchId,
                EventName = e.EventName,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Notes = e.Notes,
                CreatedOn = (DateTime)e.CreatedOn,
                Facilities = e.eventfacilities.Select(f => new EventFacilityDto
                {
                    Id = f.Id,
                    FacilityId = (int)f.FacilityId,
                    FacilityName = f.Facility.Name,
                    Quantity = (int)f.Quantity,
                    Rate = (int)f.Rate
                }).ToList()
            };
        }

        public async Task<EventDto> CreateAsync(CreateEventRequest request)
        {
            var e = new Event
            {
                CustomerId = request.CustomerId,
                BranchId = request.BranchId,
                EventName = request.EventName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Notes = request.Notes,
                CreatedOn = DateTime.UtcNow
            };

            _context.events.Add(e);
            await _context.SaveChangesAsync(); // To get Event.Id

            foreach (var f in request.Facilities)
            {
                var ef = new eventfacility
                {
                    EventId = e.Id,
                    FacilityId = f.FacilityId,
                    Quantity = f.Quantity,
                    Rate = f.Rate,
                    Total = f.Quantity * f.Rate
                };
                _context.eventfacilities.Add(ef);
            }

            await _context.SaveChangesAsync();

            return await GetByIdAsync(e.Id) ?? throw new Exception("Event creation failed.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.events
                .Include(ev => ev.eventfacilities)
                .FirstOrDefaultAsync(ev => ev.Id == id);

            if (e == null) return false;

            _context.eventfacilities.RemoveRange(e.eventfacilities);
            _context.events.Remove(e);
            await _context.SaveChangesAsync();

            return true;
        }*/
    }
}
