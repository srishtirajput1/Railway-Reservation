using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class PassengerDetail
{
    public string PassengerId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? CoachNumber { get; set; }

    public int? SeatNumber { get; set; }

    public bool BookingStatus { get; set; }

    public string TicketId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual Ticket Ticket { get; set; }

    public virtual User? User { get; set; }
}
