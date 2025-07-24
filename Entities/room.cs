using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class room
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Capacity { get; set; }

    public decimal? PricePerDay { get; set; }

    public virtual ICollection<booking> bookings { get; set; } = new List<booking>();

    public virtual ICollection<roomallocation> roomallocations { get; set; } = new List<roomallocation>();
}
