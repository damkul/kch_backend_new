using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class facility
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Included { get; set; }

    public decimal? ExtraCharge { get; set; }

    public virtual ICollection<eventfacility> eventfacilities { get; set; } = new List<eventfacility>();
}
