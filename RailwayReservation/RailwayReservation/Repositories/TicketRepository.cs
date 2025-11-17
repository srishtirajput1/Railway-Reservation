using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Repositories
{
    public class TicketRepository : ITicket
    {
        private readonly TimeSpan _cancellationTicket = TimeSpan.FromHours(24);


        private readonly OnlineRailwayReservationSystemDbContext _context;
        public TicketRepository(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChangesAsync();
            return ticket;
        }
        public async Task<Ticket> GetTicketById(string ticketId)
        {
            var ticketById = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
            if (ticketById == null)
            {
                return null;
            }

            return ticketById;
        }


        // Method to cancel a ticket by its TicketId within a time limit (e.g., 24 hours before journey start)

        public async Task<bool> CancelTicket(string ticketId)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);

            if (ticket == null)
            {
                return false;
            }
            var timeRemaining = ticket.JourneyStartDate - DateTime.UtcNow;

            // Check if the cancellation is allowed (more than 24 hours before journey start)
            if (timeRemaining.TotalHours <= 24)
            {
                return false;
            }

            ticket.TicketStatus = "Cancelled";

            // Optionally, you can add logic to handle payment or refund if applicable
            // e.g., mark payments as refunded or process refund logic here
            _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Ticket>> GetByStatus(string status)
        {
            return _context.Tickets.Where(t => t.TicketStatus == status).ToList();
        }
    }
}

