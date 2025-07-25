using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("bill")]
public partial class Bill
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }

    public int EventId { get; set; }

    public decimal? HallCharge { get; set; }

    public decimal? FacilityCharge { get; set; }

    public decimal? CateringCharge { get; set; }

    public decimal? DecorationCharge { get; set; }

    public decimal? Tax { get; set; }

    public decimal? Discount { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual Event Event { get; set; } = null!;
}
