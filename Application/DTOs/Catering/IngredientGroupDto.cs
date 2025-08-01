namespace kch_backend.Application.DTOs.Catering
{
    public class IngredientGroupDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal TotalQuantity { get; set; }
        public string DisplayQuantity { get; set; }
    }
}
