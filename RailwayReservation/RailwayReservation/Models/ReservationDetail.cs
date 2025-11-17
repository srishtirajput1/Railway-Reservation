using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class ReservationDetail
{
    public string ReservationId { get; set; } = null!;

    public string TicketId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string Address { get; set; } = null!;

    public string PaymentId { get; set; } = null!;

    public string TrainId { get; set; }

    public string PassengerId { get; set; }

    public int PnrNumber { get; set; }

    public string Status { get; set; }

    public decimal TotalFare { get; set; }

    public long TransactionNumber { get; set; }

    public DateTime BookingDate { get; set; }

    public string QuotaName { get; set; }

    public long SeatNumber { get; set; }

    public PassengerDetail Passenger { get; set; }

    public Payment Payment { get; set; } = null!;

    public Ticket Ticket { get; set; } = null!;

    public Train Train { get; set; }

    public User User { get; set; } = null!;
}
