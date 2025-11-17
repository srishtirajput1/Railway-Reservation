using RailwayReservation.ViewModels;

namespace RailwayReservation.Interfaces
{
    public interface IPaymentService
    {

        Task<string> ProcessPaymentAsync(PaymentRequest paymentDto);

        Task<bool> CompletePaymentAsync(string paymentId, string paymentIntentId);

    }

}

