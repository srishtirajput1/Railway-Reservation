using System.ComponentModel.DataAnnotations;
using RailwayReservation.Models;

namespace RailwayReservation.ViewModels
{
    public class TrainVM
    {
        public string TrainId { get; set; } = null!;
        public string TrainNumber { get; set; } = null!;
        public string TrainName { get; set; } = null!;
        public string? RunningDay { get; set; }
        public string? RouteId { get; set; }
        public string SourceStation { get; set; } = null!;
        public string DestinationStation { get; set; } = null!;
        public DateTime JourneyStartDate { get; set; }
        public DateTime JourneyEndDate { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableGeneralSeat { get; set; }
        public int AvailableLadiesSeat { get; set; }
        public decimal SeatFare { get; set; }

    }
}
