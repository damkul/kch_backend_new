using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Room? Room { get; set; }

    public virtual ICollection<EventMenu> eventmenus { get; set; } = new List<EventMenu>();

    public virtual ICollection<Invoice> invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<RoomAllocation> roomallocations { get; set; } = new List<RoomAllocation>();
}
