using kch_backend.Application.DTOs.Catering;
using kch_backend.Application.DTOs.Recipe;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;

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
            try
            {
                Log.Information("Assigning catering for EventId: {EventId}, RecipeId: {RecipeId}, MealType: {MealType}, People: {People}",
                    dto.EventId, dto.RecipeId, dto.MealType, dto.NumberOfPeople);

                // Step 1: Save or update EventCatering entry
                var existing = await _context.EventCaterings
                    .FirstOrDefaultAsync(x => x.EventId == dto.EventId && x.RecipeId == dto.RecipeId && x.MealType == dto.MealType);

                if (existing != null)
                {
                    existing.NumberOfPeople = dto.NumberOfPeople;
                    Log.Information("Updated existing EventCatering entry for EventId: {EventId}", dto.EventId);
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
                    Log.Information("Added new EventCatering entry for EventId: {EventId}", dto.EventId);
                }

                await _context.SaveChangesAsync();

                // Step 2: Fetch recipe and its ingredients
                var recipe = await _context.Recipes
                    .Include(r => r.recipeingredients)
                    .ThenInclude(ri => ri.Ingredient)
                    .FirstOrDefaultAsync(r => r.Id == dto.RecipeId);

                if (recipe == null || recipe.recipeingredients == null || recipe.recipeingredients.Count == 0)
                {
                    Log.Warning("Recipe or ingredients not found for RecipeId: {RecipeId}", dto.RecipeId);
                    throw new Exception("Recipe or ingredients not found.");
                }

                // Step 3: Remove existing stock entries
                var oldStock = _context.EventCateringStocks
                    .Where(x => x.EventId == dto.EventId && x.RecipeId == dto.RecipeId && x.MealType == dto.MealType);
                _context.EventCateringStocks.RemoveRange(oldStock);
                Log.Information("Removed {Count} old stock entries", await oldStock.CountAsync());

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

                    Log.Debug("Calculated stock: {Ingredient} => {Qty} {Unit}", ri.Ingredient.Name, requiredQty, ri.Ingredient.Unit);
                }

                await _context.SaveChangesAsync();
                Log.Information("Catering assigned and stock updated for EventId: {EventId}", dto.EventId);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error assigning catering for EventId: {EventId}", dto.EventId);
                throw;
            }
        }

        public async Task<List<CateringStockDto>> GetStockByEventAsync(int eventId)
        {
            try
            {
                Log.Information("Fetching catering stock for EventId: {EventId}", eventId);

                var result = await _context.EventCateringStocks
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

                Log.Information("Fetched {Count} catering stock records for EventId: {EventId}", result.Count, eventId);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching catering stock for EventId: {EventId}", eventId);
                throw;
            }
        }

        public async Task<List<SelectedMenuItemDto>> GetSelectedMenuByEventAsync(int eventId)
        {
            try
            {
                Log.Information("Fetching selected menu for EventId: {EventId}", eventId);

                var eventIdParam = new MySqlParameter("@inputEventId", eventId);

                // Direct SP call, no LINQ composition → safe
                var result = _context.Set<SelectedMenuItemDto>()
                    .FromSqlRaw("CALL GetSelectedMenuByEvent(@inputEventId)", eventIdParam)
                    .AsEnumerable() // ✅ Prevents EF from composing over SP
                    .ToList();

                Log.Information("Fetched {Count} selected menu items for EventId: {EventId}", result.Count, eventId);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching selected menu for EventId: {EventId}", eventId);
                throw;
            }
        }


        public async Task<List<GroupedRecipeDto>> GetGroupedDetailedMenuForEventAsync(int eventId)
        {
            try
            {
                Log.Information("Fetching grouped detailed menu for EventId: {EventId}", eventId);

                var eventIdParam = new MySqlParameter("@inputEventId", eventId);

                // Fetch flat result from stored procedure
                var flatList = _context.Set<DetailedMenuRecipeNestedDto>()
                    .FromSqlRaw("CALL GetFullDetailedMenuForEvent(@inputEventId)", eventIdParam)
                    .AsEnumerable() // ✅ Move to memory before GroupBy
                    .ToList();

                // Group by Recipe in-memory
                var grouped = flatList
                    .GroupBy(r => new
                    {
                        r.EventId,
                        r.EventName,
                        r.RecipeId,
                        r.RecipeName,
                        r.CategoryId,
                        r.CategoryName,
                        r.MealType,
                        r.NumberOfPeople,
                        r.StandardServingSize,
                        r.RecipeDescription,
                        r.CookingMethod
                    })
                    .Select(g => new GroupedRecipeDto
                    {
                        EventId = g.Key.EventId,
                        EventName = g.Key.EventName,
                        RecipeId = g.Key.RecipeId,
                        RecipeName = g.Key.RecipeName,
                        CategoryId = g.Key.CategoryId,
                        CategoryName = g.Key.CategoryName,
                        MealType = g.Key.MealType,
                        NumberOfPeople = g.Key.NumberOfPeople,
                        StandardServingSize = g.Key.StandardServingSize,
                        RecipeDescription = g.Key.RecipeDescription,
                        CookingMethod = g.Key.CookingMethod,
                        Ingredients = g.Select(i =>
                        {
                            var totalQty = Math.Round(
                                (g.Key.NumberOfPeople / (decimal)g.Key.StandardServingSize) * i.Quantity, 2
                            );
                            return new IngredientGroupDto
                            {
                                IngredientId = i.IngredientId,
                                IngredientName = i.IngredientName,
                                Quantity = i.Quantity,
                                Unit = i.Unit,
                                TotalQuantity = totalQty,
                                DisplayQuantity = ConvertToReadableUnit(totalQty, i.Unit)
                            };
                        }).ToList()
                    })
                    .ToList();

                Log.Information("Grouped detailed menu created for EventId: {EventId}", eventId);
                return grouped;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching grouped detailed menu for EventId: {EventId}", eventId);
                throw;
            }
        }



        private string ConvertToReadableUnit(decimal quantity, string unit)
        {
            if (unit.Equals("g", StringComparison.OrdinalIgnoreCase) && quantity >= 1000)
                return $"{Math.Round(quantity / 1000, 2)} kg";

            if (unit.Equals("ml", StringComparison.OrdinalIgnoreCase) && quantity >= 1000)
                return $"{Math.Round(quantity / 1000, 2)} l";

            return $"{quantity} {unit}";
        }


    }
}
