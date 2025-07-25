using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;


[Table("recipe")]
public partial class Recipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Method { get; set; }

    public int StandardServingSize { get; set; }

    public virtual RecipeCategory Category { get; set; } = null!;

    public virtual ICollection<EventCatering> eventcaterings { get; set; } = new List<EventCatering>();

    public virtual ICollection<EventCateringStock> eventcateringstocks { get; set; } = new List<EventCateringStock>();

    public virtual ICollection<RecipeIngredient> recipeingredients { get; set; } = new List<RecipeIngredient>();
}
