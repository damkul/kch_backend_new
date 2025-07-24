namespace kch_backend.Application.DTOs.Event
{
    public class EventFacilityDto
    {
        public int? Id { get; set; }
        public int FacilityId { get; set; }
        public string FacilityName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Total => Quantity * Rate;
    }
}
