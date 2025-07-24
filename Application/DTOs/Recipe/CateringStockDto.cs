namespace kch_backend.Application.DTOs.Recipe
{
    public class CateringStockDto
    {
        public int EventId { get; set; }
        public int RecipeId { get; set; }
        public string MealType { get; set; } = string.Empty;
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal RequiredQuantity { get; set; }
    }
}
