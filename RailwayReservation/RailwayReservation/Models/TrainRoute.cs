using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class TrainRoute
{
    public string RouteId { get; set; } = null!;

    public string? Source { get; set; }

    public TimeOnly? Departure { get; set; }

    public string? Destination { get; set; }

    public TimeOnly? Arrival { get; set; }

    public int Distance { get; set; }

    public string Duration { get; set; } = null!;

    public virtual ICollection<TrainClass> TrainClasses { get; set; } = new List<TrainClass>();

    public virtual ICollection<Train> Trains { get; set; } = new List<Train>();
}
