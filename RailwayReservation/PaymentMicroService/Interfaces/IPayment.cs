using PaymentMicroService.ViewModels;

namespace PaymentMicroService.Interfaces
{
    public interface IPayment
    {
        Task<string> ProcessPaymentAsync(PaymentRequest paymentDto);

        Task<bool> CompletePaymentAsync(string paymentId, string paymentIntentId);
    }
}
