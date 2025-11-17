using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int SeatNumber { get; set; }

    public bool AvailabilityStatus { get; set; }

    public string? Quota { get; set; }

    public int? ClassCoachId { get; set; }

    public virtual ClassCoach? ClassCoach { get; set; }
}
