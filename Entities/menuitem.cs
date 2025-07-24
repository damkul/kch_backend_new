using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class menuitem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<eventmenu> eventmenus { get; set; } = new List<eventmenu>();

    public virtual ICollection<recipe> recipes { get; set; } = new List<recipe>();
}
