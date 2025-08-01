namespace kch_backend.Application.DTOs.Catering
{
    public class SelectedMenuItemDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string CategoryName { get; set; }
        public string MealType { get; set; }
        public int NumberOfPeople { get; set; }
        public int StandardServingSize { get; set; }
        public string? Description { get; set; }
    }
}
