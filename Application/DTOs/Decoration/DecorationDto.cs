namespace kch_backend.Application.DTOs.Decoration
{
    public class DecorationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Rate { get; set; }
        public bool IsDefault { get; set; }
    }
}
