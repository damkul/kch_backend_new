namespace kch_backend.Application.DTOs.Reports
{
    public class StockRequirementDto
    {
        public int EventId { get; set; }
        public string IngredientName { get; set; } = "";
        public string Unit { get; set; } = "";
        public decimal TotalRequiredQuantity { get; set; }
    }
}
