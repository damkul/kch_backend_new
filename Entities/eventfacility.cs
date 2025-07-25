using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventfacility")]
public partial class EventFacility
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int? FacilityId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Total { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Facility? Facility { get; set; }
}
