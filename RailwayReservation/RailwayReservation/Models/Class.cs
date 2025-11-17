using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class Class
{
    public string ClassId { get; set; } = null!;

    public string ClassType { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public int AvailableSeats { get; set; }

    public virtual ICollection<Fare> Fares { get; set; } = new List<Fare>();

    public virtual ICollection<TrainClass> TrainClasses { get; set; } = new List<TrainClass>();
}
