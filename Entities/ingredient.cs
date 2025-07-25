using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("ingredients")]
public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public virtual ICollection<EventCateringStock> eventcateringstocks { get; set; } = new List<EventCateringStock>();

    public virtual ICollection<RecipeIngredient> recipeingredients { get; set; } = new List<RecipeIngredient>();
}
