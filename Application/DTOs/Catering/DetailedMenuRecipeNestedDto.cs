namespace kch_backend.Application.DTOs.Catering
{
    public class DetailedMenuRecipeNestedDto
    {
        // Original DB fields (unchanged so FromSqlRaw still works)
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string MealType { get; set; }
        public int NumberOfPeople { get; set; }
        public int StandardServingSize { get; set; }
        public string RecipeDescription { get; set; }
        public string CookingMethod { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public decimal Quantity { get; set; } // base quantity from recipe
        public string Unit { get; set; }

        // New computed fields (not from DB)
        public decimal TotalQuantity { get; set; } // For event (scaled)
        public string DisplayQuantity { get; set; } // Human-readable unit, e.g., "25 kg"
    }
}
