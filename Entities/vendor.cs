using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class vendor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ServiceType { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<vendorpayment> vendorpayments { get; set; } = new List<vendorpayment>();
}
