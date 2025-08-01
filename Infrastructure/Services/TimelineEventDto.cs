namespace kch_backend.Infrastructure.Services
{
    public class TimelineEventDto
    {
        public string Category { get; set; } = "";
        public int Id { get; set; }
        public string EventName { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CustomerName { get; set; } = "";
        public string BranchName { get; set; } = "";
    }

}
