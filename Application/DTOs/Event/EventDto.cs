namespace kch_backend.Application.DTOs.Event
{
    public class EventDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedOn { get; set; }

        public List<EventFacilityDto> Facilities { get; set; } = new();
    }
}
