using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class vendorpayment
{
    public int Id { get; set; }

    public int? VendorId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Purpose { get; set; }

    public virtual vendor? Vendor { get; set; }
}
