using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public int? BranchId { get; set; }

    public string? Aadhaar { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Address { get; set; }

    public virtual branch? Branch { get; set; }

    public virtual ICollection<booking> bookings { get; set; } = new List<booking>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

}
