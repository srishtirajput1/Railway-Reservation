using System.ComponentModel.DataAnnotations;

namespace RailwayReservation.ViewModels
{
    public class PassengerDTO
    {
        public string Gender { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
    }
}
