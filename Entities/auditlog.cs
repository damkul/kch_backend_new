using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("auditlogs")]
public partial class Auditlog
{
    public int Id { get; set; }

    public decimal? TotalIncome { get; set; }

    public decimal? TotalExpense { get; set; }

    public decimal? Profit { get; set; }

    public decimal? Loss { get; set; }

    public DateTime? AuditDate { get; set; }
}
