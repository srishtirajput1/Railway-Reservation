using RailwayReservation.Models;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Interfaces
{
    public interface IReservationDetail
    {
        Ticket CancelTicket(int pnr);
        Ticket GetTicket(int pnr, string role, string userId);
        Ticket AddReservation(ReservationDTO reservation, string userId, List<PassengerDetail> passengers);
        bool DeleteReservation(int pnr);


    }
}
