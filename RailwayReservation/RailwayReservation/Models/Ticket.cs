using System;
using System.Collections.Generic;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Models;

public partial class Ticket
{
    public string TicketId { get; set; } = null!;

    public string UserId { get; set; }

    public string TrainId { get; set; }

    public int Pnr { get; set; } 

    public TimeOnly Duration { get; set; }

    public string SourceStation { get; set; } = null!;

    public string DestinationStation { get; set; } = null!;

    public string TicketStatus { get; set; } = null!;

    public decimal TotalFare { get; set; }

    public DateTime JourneyStartDate { get; set; }

    public DateTime JourneyEndDate { get; set; }

    public string SeatNumber { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public string Coach { get; set; } = null!;

    public string TrainNumber { get; set; }

    public string TrainName { get; set; }

    public DateTime BookingDate { get; set; }

    public long TransactionNumber { get; set; }

    public string QuotaName { get; set; }

    public virtual ICollection<PassengerDetail> PassengerDetails { get; set; } = new List<PassengerDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public Train Train { get; set; }

    public User User { get; set; }

    public List<PassengerTicket> PassengerTicket { get; set; }
}
