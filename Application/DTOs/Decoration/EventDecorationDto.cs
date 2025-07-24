namespace kch_backend.Application.DTOs.Decoration
{
    public class EventDecorationDto
    {
        public int? Id { get; set; }
        public int DecorationId { get; set; }
        public string DecorationName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public bool IsChargeable { get; set; }
        public decimal Total => IsChargeable ? Quantity * Rate : 0;
    }
}
