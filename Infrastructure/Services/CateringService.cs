using kch_backend.Application.DTOs.Recipe;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace kch_backend.Infrastructure.Services
{
    public class CateringService : ICateringService
    {
        private readonly KchDbContext _context;

        public CateringService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AssignCateringAsync(EventCateringDto dto)
        {
            // Step 1: Save or update EventCatering entry
            var existing = await _context.EventCaterings
                .FirstOrDefaultAsync(x => x.EventId == dto.EventId && x.RecipeId == dto.RecipeId && x.MealType == dto.MealType);

            if (existing != null)
            {
                existing.NumberOfPeople = dto.NumberOfPeople;
            }
            else
            {
                _context.EventCaterings.Add(new EventCatering
                {
                    EventId = dto.EventId,
                    RecipeId = dto.RecipeId,
                    MealType = dto.MealType,
                    NumberOfPeople = dto.NumberOfPeople
                });
            }

            await _context.SaveChangesAsync();

            // Step 2: Fetch recipe and its ingredients
            var recipe = await _context.Recipes
                .Include(r => r.recipeingredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == dto.RecipeId);

            if (recipe == null || recipe.recipeingredients.Count == 0)
                throw new Exception("Recipe or ingredients not found.");

            // Step 3: Remove existing stock entries for the same event+recipe+mealType
            var oldStock = _context.EventCateringStocks
                .Where(x => x.EventId == dto.EventId && x.RecipeId == dto.RecipeId && x.MealType == dto.MealType);
            _context.EventCateringStocks.RemoveRange(oldStock);

            // Step 4: Calculate required stock
            foreach (var ri in recipe.recipeingredients)
            {
                decimal requiredQty = (dto.NumberOfPeople / (decimal)recipe.StandardServingSize) * ri.Quantity;

                _context.EventCateringStocks.Add(new EventCateringStock
                {
                    EventId = dto.EventId,
                    RecipeId = dto.RecipeId,
                    MealType = dto.MealType,
                    IngredientId = ri.IngredientId,
                    RequiredQuantity = Math.Round(requiredQty, 2),
                    Unit = ri.Ingredient.Unit
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CateringStockDto>> GetStockByEventAsync(int eventId)
        {
            return await _context.EventCateringStocks
                .Where(x => x.EventId == eventId)
                .Include(x => x.Ingredient)
                .Select(x => new CateringStockDto
                {
                    EventId = x.EventId,
                    RecipeId = x.RecipeId,
                    MealType = x.MealType,
                    IngredientId = x.IngredientId,
                    IngredientName = x.Ingredient.Name,
                    Unit = x.Unit,
                    RequiredQuantity = x.RequiredQuantity
                }).ToListAsync();
        }
    }
}
