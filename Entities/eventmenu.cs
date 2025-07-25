using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace kch_backend.Entities;

[Table("eventmenu")]
public partial class EventMenu
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public int? MenuItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual MenuItem? MenuItem { get; set; }
}
