using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("branch")]
public partial class Branch
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? ManagerName { get; set; }

    public string? Contact { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Customer> customers { get; set; } = new List<Customer>();

    public virtual ICollection<Event> events { get; set; } = new List<Event>();
}
