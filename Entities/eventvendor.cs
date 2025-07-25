using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventvendor")]
public partial class EventVendor
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int VendorId { get; set; }

    public string? ServiceDescription { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;

    public virtual ICollection<VendorPayment> vendorpayments { get; set; } = new List<VendorPayment>();
}
