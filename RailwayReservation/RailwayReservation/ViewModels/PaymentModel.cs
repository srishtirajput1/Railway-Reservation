namespace RailwayReservation.ViewModels
{
    public class PaymentModel
    {
        public string UserId { get; set; }
        public string TicketId { get; set; }
        public double Amount { get; set; }
        public string PaymentMode { get; set; }
    }
}
