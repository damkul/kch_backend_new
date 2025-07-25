using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;


[Table("menuitem")]
public partial class MenuItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EventMenu> eventmenus { get; set; } = new List<EventMenu>();
}
