using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("invoices")]
public partial class Invoice
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public decimal? Total { get; set; }

    public decimal? Tax { get; set; }

    public decimal? GrandTotal { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual ICollection<InvoiceItem> invoiceitems { get; set; } = new List<InvoiceItem>();
}
