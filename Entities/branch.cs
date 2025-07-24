using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class branch
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? ManagerName { get; set; }

    public string? Contact { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<customer> customers { get; set; } = new List<customer>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
