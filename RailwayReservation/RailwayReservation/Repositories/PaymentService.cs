using Microsoft.EntityFrameworkCore;

using RailwayReservation.ViewModels;

using RailwayReservation.Interfaces;

using RailwayReservation.Models;
using Stripe;

namespace RailwayReservation.Repositories

{

    public class PaymentService : IPaymentService

    {

        private readonly OnlineRailwayReservationSystemDbContext _context;

        public PaymentService(OnlineRailwayReservationSystemDbContext context)

        {

            _context = context;

        }

        // 1️⃣ Create PaymentIntent & return ClientSecret

        public async Task<string> ProcessPaymentAsync(PaymentRequest paymentDto)

        {

            try

            {

                StripeConfiguration.ApiKey = "put your stripe key here"; // Replace with actual key

                var options = new PaymentIntentCreateOptions

                {

                    Amount = (long)(paymentDto.Amount * 100), // Convert to cents

                    Currency = "usd",

                    PaymentMethodTypes = new List<string> { "card" },

                    PaymentMethod = paymentDto.PaymentMethod,

                    Confirm = true

                };

                var service = new PaymentIntentService();

                var paymentIntent = await service.CreateAsync(options);

                return paymentIntent.ClientSecret; // ✅ Send to frontend

            }

            catch (Exception ex)

            {

                Console.WriteLine($"Error creating payment: {ex.Message}");

                return string.Empty;

            }

        }

        // 2️⃣ Verify Payment & Update Payment Table

        public async Task<bool> CompletePaymentAsync(string paymentId, string paymentIntentId)

        {

            try

            {

                StripeConfiguration.ApiKey = "put your stripe key here"; // Replace with actual key

                var service = new PaymentIntentService();

                var paymentIntent = await service.GetAsync(paymentIntentId); // ✅ Fetch PaymentIntent from Stripe

                if (paymentIntent == null)

                {

                    Console.WriteLine($"Error: PaymentIntent {paymentIntentId} not found on Stripe.");

                    return false;

                }

                // ✅ Fetch Payment Record from DB

                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);

                if (payment == null)

                {

                    Console.WriteLine($"Error: Payment record {paymentId} not found in DB.");

                    return false;

                }

                // ✅ Update Payment Status based on Stripe response

                if (paymentIntent.Status == "succeeded")

                {

                    payment.Status = "Successful";

                }

                else

                {

                    payment.Status = "Failed";

                }

                _context.Payments.Update(payment);

                await _context.SaveChangesAsync(); // ✅ Save updated payment status to DB

                Console.WriteLine($"Payment {paymentId} updated successfully to {payment.Status}");

                return true;

            }

            catch (Exception ex)

            {

                Console.WriteLine($"Error verifying payment: {ex.Message}");

                return false;

            }

        }

    }

}

