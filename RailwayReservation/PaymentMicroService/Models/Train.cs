using System;
using System.Collections.Generic;

namespace PaymentMicroService.Models;

public partial class Train
{
    public string TrainId { get; set; } = null!;

    public string TrainNumber { get; set; } = null!;

    public string TrainName { get; set; } = null!;

    public string? RunningDay { get; set; }

    public string? RouteId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
