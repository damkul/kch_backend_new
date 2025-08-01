namespace kch_backend.Application.DTOs.Home
{
    public class EventDetailDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
