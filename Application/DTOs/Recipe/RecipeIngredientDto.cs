namespace kch_backend.Application.DTOs.Recipe
{
    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; } // for standard serving
    }
}
