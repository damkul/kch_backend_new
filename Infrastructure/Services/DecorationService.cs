using kch_backend.Application.DTOs.Decoration;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class DecorationService : IDecorationService
    {
        private readonly KchDbContext _context;

        public DecorationService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<DecorationDto>> GetAllDecorationsAsync()
        {
            try
            {
                Log.Information("Fetching all decorations");
                var decorations = await _context.Decorations
                    .Select(d => new DecorationDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        Rate = d.Rate ?? 0,
                        IsDefault = d.IsDefault ?? false
                    }).ToListAsync();

                Log.Information("Fetched {Count} decorations", decorations.Count);
                return decorations;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching decorations");
                throw;
            }
        }

        public async Task<List<EventDecorationDto>> GetEventDecorationsAsync(int eventId)
        {
            try
            {
                Log.Information("Fetching decorations for EventId: {EventId}", eventId);
                var decorations = await _context.Eventdecorations
                    .Where(e => e.EventId == eventId)
                    .Include(e => e.Decoration)
                    .Select(ed => new EventDecorationDto
                    {
                        Id = ed.Id,
                        DecorationId = ed.DecorationId,
                        DecorationName = ed.Decoration.Name,
                        Quantity = ed.Quantity ?? 0,
                        Rate = ed.Rate ?? 0,
                        IsChargeable = ed.IsChargeable ?? false
                    }).ToListAsync();

                Log.Information("Fetched {Count} decorations for event {EventId}", decorations.Count, eventId);
                return decorations;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching event decorations for EventId: {EventId}", eventId);
                throw;
            }
        }

        public async Task<bool> AssignDecorationsAsync(int eventId, List<EventDecorationDto> decorations)
        {
            try
            {
                Log.Information("Assigning {Count} decorations to EventId: {EventId}", decorations.Count, eventId);

                var existing = _context.Eventdecorations.Where(e => e.EventId == eventId);
                _context.Eventdecorations.RemoveRange(existing);

                foreach (var d in decorations)
                {
                    _context.Eventdecorations.Add(new EventDecoration
                    {
                        EventId = eventId,
                        DecorationId = d.DecorationId,
                        Quantity = d.Quantity,
                        Rate = d.Rate,
                        IsChargeable = d.IsChargeable
                    });
                }

                await _context.SaveChangesAsync();
                Log.Information("Decorations assigned successfully to EventId: {EventId}", eventId);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while assigning decorations to EventId: {EventId}", eventId);
                throw;
            }
        }
    }
}
