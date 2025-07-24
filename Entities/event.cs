using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("event")]  // maps to lowercase table
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

    public virtual branch Branch { get; set; } = null!;

    public virtual customer Customer { get; set; } = null!;

    public virtual ICollection<eventfacility> eventfacilities { get; set; } = new List<eventfacility>();

    

}
