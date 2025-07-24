namespace kch_backend.Application.DTOs.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Method { get; set; }
        public int StandardServingSize { get; set; }

        public List<RecipeIngredientDto> Ingredients { get; set; } = new();
    }
}
