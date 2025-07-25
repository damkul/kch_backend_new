using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("customer")]
public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public int? BranchId { get; set; }

    public string? Aadhaar { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Address { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<Booking> bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Event> events { get; set; } = new List<Event>();
}
