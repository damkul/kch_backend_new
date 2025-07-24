using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class invoiceitem
{
    public int Id { get; set; }

    public int? InvoiceId { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public virtual invoice? Invoice { get; set; }
}
