using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class stockusage
{
    public int Id { get; set; }

    public int? StockItemId { get; set; }

    public string? UsedIn { get; set; }

    public decimal? QuantityUsed { get; set; }

    public DateTime? UsedDate { get; set; }

    public virtual stockitem? StockItem { get; set; }
}
