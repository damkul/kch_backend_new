namespace kch_backend.Application.DTOs.Reports
{
    public class EventSummaryDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string BranchName { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FacilityNames { get; set; } = "";
        public string CateringItems { get; set; } = "";
    }
}
