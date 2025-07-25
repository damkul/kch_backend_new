using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventcateringstock")]
public partial class EventCateringStock
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int RecipeId { get; set; }

    public string MealType { get; set; } = null!;

    public int IngredientId { get; set; }

    public decimal RequiredQuantity { get; set; }

    public string Unit { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
