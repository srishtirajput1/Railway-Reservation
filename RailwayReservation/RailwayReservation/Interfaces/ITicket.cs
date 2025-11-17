using RailwayReservation.Models;

namespace RailwayReservation.Interfaces
{
    public interface ITicket
    {
        Task<Ticket> AddTicket(Ticket ticket);
        Task<Ticket> GetTicketById(string ticketId);
        Task<bool> CancelTicket(string ticketId);
        Task<IEnumerable<Ticket>> GetByStatus(string status);
    }
}
