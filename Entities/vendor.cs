using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("Vendors")]
public partial class Vendor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string? ContactPerson { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? GstNumber { get; set; }

    public virtual VendorCategory Category { get; set; } = null!;

    public virtual ICollection<EventVendor> eventvendors { get; set; } = new List<EventVendor>();
}
