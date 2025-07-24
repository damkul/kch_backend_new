using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class bill
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }
}
