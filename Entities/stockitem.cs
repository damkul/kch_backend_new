using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("stockitem")]
public partial class StockItem
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public decimal? CostPerUnit { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual ICollection<StockUsage> stockusages { get; set; } = new List<StockUsage>();
}
