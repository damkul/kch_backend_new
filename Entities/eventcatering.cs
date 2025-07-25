using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventcatering")]
public partial class EventCatering
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int RecipeId { get; set; }

    public string MealType { get; set; } = null!;

    public int NumberOfPeople { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
