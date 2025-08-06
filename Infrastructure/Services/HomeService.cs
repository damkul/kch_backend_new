using kch_backend.Application.DTOs.Home;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace kch_backend.Infrastructure.Services
{
    public class HomeService : IHomeService
    {
        private readonly KchDbContext _context;

        public HomeService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<HomeEventStatsDto> GetEventStatsForMonthAsync()
        {
            var stats = new HomeEventStatsDto();

            // 1️⃣ Get current month events
            stats.CurrentMonthEvents = _context.CurrentMonthEvents
                .FromSqlRaw("CALL GetCurrentMonthEvents()")
                .AsEnumerable()
                .ToList();

            // 2️⃣ Get previous month count
            var prevCount = _context.EventCounts
                .FromSqlRaw("CALL GetPreviousMonthEvents()")
                .AsEnumerable()
                .FirstOrDefault();
            stats.PreviousMonthCount = prevCount?.Count ?? 0;

            // 3️⃣ Get next month count
            var nextCount = _context.EventCounts
                .FromSqlRaw("CALL GetNextMonthEvents()")
                .AsEnumerable()
                .FirstOrDefault();
            stats.NextMonthCount = nextCount?.Count ?? 0;

            return stats;
        }

    }
}
