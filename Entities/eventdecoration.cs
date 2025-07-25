using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventdecoration")]
public partial class EventDecoration
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int DecorationId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Rate { get; set; }

    public bool? IsChargeable { get; set; }

    public decimal? Total { get; set; }

    public virtual Decoration Decoration { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
