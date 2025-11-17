using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class TrainQuotum
{
    public string TrainId { get; set; } = null!;

    public string QuotaId { get; set; } = null!;

    public virtual Quotum Quota { get; set; } = null!;

    public virtual Train Train { get; set; } = null!;
}
