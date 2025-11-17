using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.ViewModels
{
    public class PassengerTicket
    {
        [Key]  // Ensure a primary key is defined
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public long Seat { get; set; }
    }
}
