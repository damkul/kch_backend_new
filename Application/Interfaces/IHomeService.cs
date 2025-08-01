using System.Threading.Tasks;
using kch_backend.Application.DTOs.Home;

namespace kch_backend.Application.Interfaces
{
    public interface IHomeService
    {
        Task<HomeEventStatsDto> GetEventStatsForMonthAsync();
    }
}
