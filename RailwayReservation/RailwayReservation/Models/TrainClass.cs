using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class TrainClass
{
    public int TrainClassId { get; set; }

    public string? TrainId { get; set; }

    public string? ClassId { get; set; }

    public string? RouteId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<ClassCoach> ClassCoaches { get; set; } = new List<ClassCoach>();

    public virtual TrainRoute? Route { get; set; }

    public virtual Train? Train { get; set; }
}
