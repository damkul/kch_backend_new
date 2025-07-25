using kch_backend.Application.DTOs.Event;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly KchDbContext _context;

        public EventService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            try
            {
                Log.Information("Fetching all events");
                var events = await _context.events
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
                            FacilityId = f.FacilityId ?? 0,
                            FacilityName = f.Facility.Name,
                            Quantity = f.Quantity ?? 0,
                            Rate = f.Rate ?? 0
                        }).ToList()
                    }).ToListAsync();

                Log.Information("Fetched {Count} events", events.Count);
                return events;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching events");
                throw;
            }
        }

        public async Task<EventDto?> GetByIdAsync(int id)
        {
            try
            {
                Log.Information("Fetching event with ID: {Id}", id);

                var e = await _context.events
                    .Include(ev => ev.eventfacilities)
                    .ThenInclude(ef => ef.Facility)
                    .FirstOrDefaultAsync(ev => ev.Id == id);

                if (e == null)
                {
                    Log.Warning("Event not found with ID: {Id}", id);
                    return null;
                }

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
                        FacilityId = f.FacilityId ?? 0,
                        FacilityName = f.Facility.Name,
                        Quantity = f.Quantity ?? 0,
                        Rate = f.Rate ?? 0
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching event with ID: {Id}", id);
                throw;
            }
        }

        public async Task<EventDto> CreateAsync(CreateEventRequest request)
        {
            try
            {
                Log.Information("Creating new event for customer {CustomerId}", request.CustomerId);

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
                await _context.SaveChangesAsync(); // Get ID

                foreach (var f in request.Facilities)
                {
                    var ef = new EventFacility
                    {
                        EventId = e.Id,
                        FacilityId = f.FacilityId,
                        Quantity = f.Quantity,
                        Rate = f.Rate,
                        Total = f.Quantity * f.Rate
                    };
                    _context.Eventfacilities.Add(ef);
                }

                await _context.SaveChangesAsync();

                Log.Information("Event created successfully with ID: {Id}", e.Id);

                return await GetByIdAsync(e.Id) ?? throw new Exception("Event creation failed.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while creating event for customer {CustomerId}", request.CustomerId);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Log.Information("Deleting event with ID: {Id}", id);

                var e = await _context.events
                    .Include(ev => ev.eventfacilities)
                    .FirstOrDefaultAsync(ev => ev.Id == id);

                if (e == null)
                {
                    Log.Warning("Event not found for deletion with ID: {Id}", id);
                    return false;
                }

                _context.Eventfacilities.RemoveRange(e.eventfacilities);
                _context.events.Remove(e);
                await _context.SaveChangesAsync();

                Log.Information("Event deleted successfully with ID: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting event with ID: {Id}", id);
                throw;
            }
        }
    }
}
