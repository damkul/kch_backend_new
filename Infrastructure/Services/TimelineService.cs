using kch_backend.Application.Interfaces;
using kch_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace kch_backend.Infrastructure.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly KchDbContext _context;

        public TimelineService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimelineEventDto>> GetEventTimelineAsync()
        {
            var result = new List<TimelineEventDto>();

            var upcoming = await _context.Set<TimelineEventDto>()
                .FromSqlRaw("CALL GetUpcomingEvents()").ToListAsync();
            upcoming.ForEach(e => e.Category = "Upcoming");

            var current = await _context.Set<TimelineEventDto>()
                .FromSqlRaw("CALL GetCurrentMonthEvents()").ToListAsync();
            current.ForEach(e => e.Category = "CurrentMonth");

            var next = await _context.Set<TimelineEventDto>()
                .FromSqlRaw("CALL GetNextMonthEvents()").ToListAsync();
            next.ForEach(e => e.Category = "NextMonth");

            var prev = await _context.Set<TimelineEventDto>()
                .FromSqlRaw("CALL GetPreviousMonthEvents()").ToListAsync();
            prev.ForEach(e => e.Category = "PreviousMonth");

            result.AddRange(upcoming);
            result.AddRange(current);
            result.AddRange(next);
            result.AddRange(prev);

            return result;
        }
    }

}
