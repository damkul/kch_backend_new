using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("stockusage")]
public partial class StockUsage
{
    public int Id { get; set; }

    public int? StockItemId { get; set; }

    public string? UsedIn { get; set; }

    public decimal? QuantityUsed { get; set; }

    public DateTime? UsedDate { get; set; }

    public virtual StockItem? StockItem { get; set; }
}
