using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("vendorpayment")]
public partial class VendorPayment
{
    public int Id { get; set; }

    public int EventVendorId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal AmountPaid { get; set; }

    public string? PaymentMode { get; set; }

    public string? Remarks { get; set; }

    public virtual EventVendor EventVendor { get; set; } = null!;
}
