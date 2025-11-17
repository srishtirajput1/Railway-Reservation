using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public class Support
{
    public string SupportId { get; set; } = null!;

    public string UserId { get; set; }

    public string QueryListId { get; set; }

    public string QueryText { get; set; }

    public string Status { get; set; }

    public QueryList QueryList { get; set; }
    public string Query { get; set; }

    public virtual User User { get; set; }
}
