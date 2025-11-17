using RailwayReservation.Models;

namespace RailwayReservation.ViewModels
{
    public class ReservationDTO
    {
        public string TrainId { get; set; }
        public string QuotaName { get; set; }
        public List<PassengerDTO> Passengers { get; set; }
        public Transaction Transactions { get; set; }
    }
}
