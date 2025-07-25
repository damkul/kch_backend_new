using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("roomallocations")]
public partial class RoomAllocation
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public int? RoomId { get; set; }

    public bool? IsExtra { get; set; }

    public decimal? Charge { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Room? Room { get; set; }
}
