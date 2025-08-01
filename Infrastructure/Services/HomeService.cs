using kch_backend.Application.DTOs.Home;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetEventStatsForMonth";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // First result: Current Month Events
                        while (await reader.ReadAsync())
                        {
                            stats.CurrentMonthEvents.Add(new EventDetailDto
                            {
                                Id = reader.GetInt32(0),
                                EventName = reader.GetString(1),
                                CustomerId = reader.GetInt32(2),
                                BranchId = reader.GetInt32(3),
                                StartDate = reader.GetDateTime(4),
                                EndDate = reader.GetDateTime(5),
                                Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                                CreatedOn = reader.GetDateTime(7)
                            });
                        }

                        // Move to next result set
                        await reader.NextResultAsync();

                        // Second result: Previous month count
                        if (await reader.ReadAsync())
                        {
                            stats.PreviousMonthCount = reader.GetInt32(0);
                        }

                        // Move to next result set
                        await reader.NextResultAsync();

                        // Third result: Next month count
                        if (await reader.ReadAsync())
                        {
                            stats.NextMonthCount = reader.GetInt32(0);
                        }
                    }
                }
            }

            return stats;
        }
    }
}
