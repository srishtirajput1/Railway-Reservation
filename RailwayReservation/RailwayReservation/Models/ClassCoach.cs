using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class ClassCoach
{
    public int ClassCoachId { get; set; }

    public int? TrainClassId { get; set; }

    public string? CoachId { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual TrainClass? TrainClass { get; set; }
}
