using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class auditlog
{
    public int Id { get; set; }

    public decimal? TotalIncome { get; set; }

    public decimal? TotalExpense { get; set; }

    public decimal? Profit { get; set; }

    public decimal? Loss { get; set; }

    public DateTime? AuditDate { get; set; }
}
