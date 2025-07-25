using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("decorations")]
public partial class Decoration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Rate { get; set; }

    public bool? IsDefault { get; set; }

    public virtual ICollection<EventDecoration> eventdecorations { get; set; } = new List<EventDecoration>();
}
