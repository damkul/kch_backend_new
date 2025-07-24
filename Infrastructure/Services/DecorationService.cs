using kch_backend.Application.DTOs.Decoration;
using kch_backend.Application.Interfaces;
using kch_backend.Data;

namespace kch_backend.Infrastructure.Services
{
    public class DecorationService : IDecorationService
    {
        /* private readonly KchDbContext _context;

         public DecorationService(KchDbContext context)
         {
             _context = context;
         }

         public async Task<List<DecorationDto>> GetAllDecorationsAsync()
         {
             return await _context.Decorations
                 .Select(d => new DecorationDto
                 {
                     Id = d.Id,
                     Name = d.Name,
                     Description = d.Description,
                     Rate = d.Rate,
                     IsDefault = d.IsDefault
                 }).ToListAsync();
         }

         public async Task<List<EventDecorationDto>> GetEventDecorationsAsync(int eventId)
         {
             return await _context.EventDecorations
                 .Where(e => e.EventId == eventId)
                 .Include(e => e.Decoration)
                 .Select(ed => new EventDecorationDto
                 {
                     Id = ed.Id,
                     DecorationId = ed.DecorationId,
                     DecorationName = ed.Decoration.Name,
                     Quantity = ed.Quantity,
                     Rate = ed.Rate,
                     IsChargeable = ed.IsChargeable
                 }).ToListAsync();
         }

         public async Task<bool> AssignDecorationsAsync(int eventId, List<EventDecorationDto> decorations)
         {
             var existing = _context.EventDecorations.Where(e => e.EventId == eventId);
             _context.EventDecorations.RemoveRange(existing);

             foreach (var d in decorations)
             {
                 _context.EventDecorations.Add(new EventDecoration
                 {
                     EventId = eventId,
                     DecorationId = d.DecorationId,
                     Quantity = d.Quantity,
                     Rate = d.Rate,
                     IsChargeable = d.IsChargeable
                 });
             }

             await _context.SaveChangesAsync();
             return true;
         }*/
        public Task<bool> AssignDecorationsAsync(int eventId, List<EventDecorationDto> decorations)
        {
            throw new NotImplementedException();
        }

        public Task<List<DecorationDto>> GetAllDecorationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<EventDecorationDto>> GetEventDecorationsAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
