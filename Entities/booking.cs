using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class booking
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual customer? Customer { get; set; }

    public virtual room? Room { get; set; }

    public virtual ICollection<eventmenu> eventmenus { get; set; } = new List<eventmenu>();

    public virtual ICollection<invoice> invoices { get; set; } = new List<invoice>();

    public virtual ICollection<roomallocation> roomallocations { get; set; } = new List<roomallocation>();
}
