namespace kch_backend.Application.DTOs.Recipe
{
    public class EventCateringDto
    {
        public int EventId { get; set; }
        public int RecipeId { get; set; }
        public string MealType { get; set; } = string.Empty; // Breakfast, Lunch, Dinner
        public int NumberOfPeople { get; set; }
    }
}
