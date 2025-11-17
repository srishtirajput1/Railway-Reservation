namespace RailwayReservation.ViewModels
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public string TicketId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMode { get; set; }

    }
}