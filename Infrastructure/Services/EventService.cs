// kch_backend.Infrastructure.Services/EventService.cs
using kch_backend.Application.DTOs.Decoration;
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
                    .Include(e => e.eventfacilities).ThenInclude(ef => ef.Facility)
                    .AsNoTracking()
                    .OrderByDescending(e => e.CreatedOn)
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
                            Quantity = (int)(f.Quantity ?? 0),
                            Rate = f.Rate ?? 0,
                           /* Notes = f.Notes*/
                            // Total is computed in DTO
                        }).ToList()
                    })
                    .ToListAsync();

                Log.Information("Fetched {Count} events", events.Count());
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
                    .Include(ev => ev.eventfacilities).ThenInclude(ef => ef.Facility)
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
                        Quantity = (int)(f.Quantity ?? 0),
                        Rate = f.Rate ?? 0,
                        /*Notes = f.Notes*/
                        // Total computed
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
                await _context.SaveChangesAsync(); // obtain Id

                foreach (var f in request.Facilities)
                {
                    var ef = new EventFacility
                    {
                        EventId = e.Id,
                        FacilityId = f.FacilityId,
                        Quantity = f.Quantity,
                        Rate = f.Rate,
                        Total = f.Quantity * f.Rate,
                        /*Notes = f.Notes*/
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
                    .Include(ev => ev.eventdecorations)
                    .FirstOrDefaultAsync(ev => ev.Id == id);

                if (e == null)
                {
                    Log.Warning("Event not found for deletion with ID: {Id}", id);
                    return false;
                }

                _context.Eventdecorations.RemoveRange(e.eventdecorations);
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

        // ================= Facilities =================

        public async Task<EventFacilityDto> AddFacilityAsync(int eventId, EventFacilityDto dto)
        {
            var ev = await _context.events.FirstOrDefaultAsync(e => e.Id == eventId)
                     ?? throw new Exception("Event not found.");

            var fac = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == dto.FacilityId)
                      ?? throw new Exception("Facility not found.");

            var ef = new EventFacility
            {
                EventId = eventId,
                FacilityId = dto.FacilityId,
                Quantity = dto.Quantity,
                Rate = dto.Rate,
                Total = dto.Quantity * dto.Rate,
               /* Notes = dto.Notes*/
            };

            _context.Eventfacilities.Add(ef);
            await _context.SaveChangesAsync();

            Log.Information("Facility {FacilityId} added to Event {EventId} (EF.Id={Id})", dto.FacilityId, eventId, ef.Id);

            // Do NOT set Total (computed in DTO)
            return new EventFacilityDto
            {
                Id = ef.Id,
                FacilityId = ef.FacilityId ?? 0,
                FacilityName = fac.Name,
                Quantity = (int)(ef.Quantity ?? 0),
                Rate = ef.Rate ?? 0,
               /* Notes = ef.Notes*/
            };
        }

        public async Task<List<EventFacilityDto>> GetFacilitiesByEventAsync(int eventId)
        {
            var list = await _context.Eventfacilities
                .AsNoTracking()
                .Include(x => x.Facility)
                .Where(x => x.EventId == eventId)
                .OrderBy(x => x.Id)
                .Select(x => new EventFacilityDto
                {
                    Id = x.Id,
                    FacilityId = x.FacilityId ?? 0,
                    FacilityName = x.Facility.Name,
                    Quantity = (int)(x.Quantity ?? 0),
                    Rate = x.Rate ?? 0,
                    /*Notes = x.Notes*/
                    // Total computed
                })
                .ToListAsync();

            Log.Information("Fetched {Count} Facilities for Event {EventId}", list.Count, eventId);
            return list;
        }

        public async Task<bool> DeleteFacilityAsync(int id)
        {
            var row = await _context.Eventfacilities.FirstOrDefaultAsync(x => x.Id == id);
            if (row == null) return false;
            _context.Eventfacilities.Remove(row);
            await _context.SaveChangesAsync();
            return true;
        }

        // ================= Decorations =================

        public async Task<EventDecorationDto> AddDecorationAsync(int eventId, EventDecorationDto dto)
        {
            var ev = await _context.events.FirstOrDefaultAsync(e => e.Id == eventId)
                     ?? throw new Exception("Event not found.");

            var dec = await _context.Decorations.FirstOrDefaultAsync(d => d.Id == dto.DecorationId)
                      ?? throw new Exception("Decoration not found.");

            var ed = new EventDecoration
            {
                EventId = eventId,
                DecorationId = dto.DecorationId,
                Quantity = dto.Quantity,
                Rate = dto.Rate,
                Total = dto.Quantity * dto.Rate,
                /*Notes = dto.Notes*/
            };

            _context.Eventdecorations.Add(ed);
            await _context.SaveChangesAsync();

            Log.Information("Decoration {DecorationId} added to Event {EventId} (ED.Id={Id})", dto.DecorationId, eventId, ed.Id);

            // Do NOT set Total (computed)
            return new EventDecorationDto
            {
                Id = ed.Id,
                DecorationId = ed.DecorationId,
                DecorationName = dec.Name,
                Quantity = (int)(ed.Quantity ?? 0),
                Rate = ed.Rate ?? 0,
               /* Notes = ed.Notes*/
            };
        }

        public async Task<List<EventDecorationDto>> GetDecorationsByEventAsync(int eventId)
        {
            var list = await _context.Eventdecorations
                .AsNoTracking()
                .Include(x => x.Decoration)
                .Where(x => x.EventId == eventId)
                .OrderBy(x => x.Id)
                .Select(x => new EventDecorationDto
                {
                    Id = x.Id,
                    DecorationId = x.DecorationId,
                    DecorationName = x.Decoration.Name,
                    Quantity = (int)(x.Quantity ?? 0),
                    Rate = x.Rate ?? 0,
                    /*Notes = x.Notes*/
                    // Total computed
                })
                .ToListAsync();

            Log.Information("Fetched {Count} Decorations for Event {EventId}", list.Count, eventId);
            return list;
        }

        public async Task<bool> DeleteDecorationAsync(int id)
        {
            var row = await _context.Eventdecorations.FirstOrDefaultAsync(x => x.Id == id);
            if (row == null) return false;
            _context.Eventdecorations.Remove(row);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}




/*using kch_backend.Application.DTOs.Event;
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
*/