using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("events")]
public partial class Event
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int BranchId { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Bill> bills { get; set; } = new List<Bill>();

    public virtual ICollection<EventCatering> eventcaterings { get; set; } = new List<EventCatering>();

    public virtual ICollection<EventCateringStock> eventcateringstocks { get; set; } = new List<EventCateringStock>();

    public virtual ICollection<EventDecoration> eventdecorations { get; set; } = new List<EventDecoration>();

    public virtual ICollection<EventFacility> eventfacilities { get; set; } = new List<EventFacility>();

    public virtual ICollection<EventVendor> eventvendors { get; set; } = new List<EventVendor>();
}
