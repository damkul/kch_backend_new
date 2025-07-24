using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class invoice
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public decimal? Total { get; set; }

    public decimal? Tax { get; set; }

    public decimal? GrandTotal { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public virtual booking? Booking { get; set; }

    public virtual ICollection<invoiceitem> invoiceitems { get; set; } = new List<invoiceitem>();
}
