using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class eventfacility
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int? FacilityId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Total { get; set; }

    public virtual Event? Event { get; set; }

    public virtual facility? Facility { get; set; }
}
