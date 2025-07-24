using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class stockitem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public decimal? CostPerUnit { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual ICollection<stockusage> stockusages { get; set; } = new List<stockusage>();
}
