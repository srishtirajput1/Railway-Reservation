using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class Train
{
    public string TrainId { get; set; } = null!;

    public string TrainNumber { get; set; } = null!;

    public string TrainName { get; set; } = null!;

    public string RunningDay { get; set; }

    public DateTime JourneyStartDate { get; set; }

    public DateTime JourneyEndDate { get; set; }

    public int TotalSeats { get; set; }

    public int AvailableGeneralSeats { get; set; }

    public int AvailableLadiesSeats { get; set; }

    public decimal SeatFare { get; set; }

    public string RouteId { get; set; }

    public string SourceStation { get; set; }

    public string DestinationStation { get; set; }

    public virtual ICollection<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    public virtual TrainRoute TrainRoute { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<TrainClass> TrainClasses { get; set; } = new List<TrainClass>();
}

