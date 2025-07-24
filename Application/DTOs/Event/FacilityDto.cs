namespace kch_backend.Application.DTOs.Event
{
    public class FacilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public decimal Rate { get; set; }
    }
}
