using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class roomallocation
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public int? RoomId { get; set; }

    public bool? IsExtra { get; set; }

    public decimal? Charge { get; set; }

    public virtual booking? Booking { get; set; }

    public virtual room? Room { get; set; }
}
