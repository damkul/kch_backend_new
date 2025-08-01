namespace kch_backend.Application.DTOs.Catering
{
    public class GroupedRecipeDto
    {
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
        public List<IngredientGroupDto> Ingredients { get; set; } = new();
    }
}
