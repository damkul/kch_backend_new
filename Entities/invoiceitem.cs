using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("invoiceitem")]
public partial class InvoiceItem
{
    public int Id { get; set; }

    public int? InvoiceId { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
