namespace kch_backend.Application.DTOs.Home
{
    public class HomeEventStatsDto
    {
        public List<EventDetailDto> CurrentMonthEvents { get; set; } = new();
        public int PreviousMonthCount { get; set; }
        public int NextMonthCount { get; set; }
    }
}
