using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class recipe
{
    public int Id { get; set; }

    public int? MenuItemId { get; set; }

    public string? Ingredient { get; set; }

    public string? Quantity { get; set; }

    public virtual menuitem? MenuItem { get; set; }
}
