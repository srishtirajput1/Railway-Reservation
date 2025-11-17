using System;
using System.Collections.Generic;

namespace RailwayReservation.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public string CardNumber { get; set; } = null!;

    public int? ExpiryYear { get; set; }

    public int? ExpiryMonth { get; set; }

    public int? Cvv { get; set; }

    public string CardHolderName { get; set; } = null!;
}
