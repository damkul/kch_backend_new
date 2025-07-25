using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("facility")]
public partial class Facility
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Included { get; set; }

    public decimal? ExtraCharge { get; set; }

    public virtual ICollection<EventFacility> eventfacilities { get; set; } = new List<EventFacility>();
}
