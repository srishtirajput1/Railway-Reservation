using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicket _ticket;
        public TicketController(ITicket ticket)
        {
            _ticket = ticket;
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById(string ticketId)
        {
            var ticket = await _ticket.GetTicketById(ticketId);

            if (ticket == null)
            {
                return NotFound(new { message = $"Ticket with ID {ticketId} not found." });
            }

            return Ok(ticket);
        }


        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatusAsync(string status)
        {
            var tickets = await _ticket.GetByStatus(status);

            // Check if no tickets found
            if (tickets == null || !tickets.Any())
            {
                return NotFound(new { message = "No tickets found with the specified status." });
            }
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody] Ticket ticket)
        {
            var newTicket = await _ticket.AddTicket(ticket);
            return Ok(ticket);
        }



        [HttpDelete("cancel/{ticketId}")]
        public async Task<IActionResult> CancelTicket(string ticketId)
        {
            var ticket = await _ticket.GetTicketById(ticketId);
            if (ticket == null)
            {
                return NotFound("Ticket not found for cancellation.");
            }

            await _ticket.CancelTicket(ticketId);
            return Ok("Ticket successfully cancelled.");
        }

        //[HttpGet("SearchTrain")]
        //public async Task<IActionResult> GetTrains([FromQuery] string? sourceStation, [FromQuery] string? destinationStation)
        //{
        //    var trains = await _ticket.GetTrains(sourceStation, destinationStation);

        //    if (trains == null || !trains.Any())
        //    {
        //        return NotFound(new { message = "No trains found matching the given criteria." });
        //    }
        //    return Ok(trains);
        //}

    }
}

