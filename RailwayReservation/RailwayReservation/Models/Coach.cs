using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class Coach
{
    public string CoachId { get; set; } = null!;

    public string? ClassId { get; set; }

    public string CoachNumber { get; set; } = null!;

    public virtual ICollection<ClassCoach> ClassCoaches { get; set; } = new List<ClassCoach>();
}
