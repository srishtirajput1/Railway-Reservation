namespace RailwayReservation.ViewModels

{
    public class RegisterModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;

    }
}
