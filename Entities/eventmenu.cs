using System;
using System.Collections.Generic;

namespace kch_backend.Entities;

public partial class eventmenu
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public int? MenuItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual booking? Booking { get; set; }

    public virtual menuitem? MenuItem { get; set; }
}
