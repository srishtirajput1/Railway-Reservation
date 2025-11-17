using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public string? Role { get; set; }

    public string? HashPassword { get; set; }

    public string? SaltPassword { get; set; }

    public virtual ICollection<PassengerDetail> PassengerDetails { get; set; } = new List<PassengerDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual ICollection<Support> Supports { get; set; } = new List<Support>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
