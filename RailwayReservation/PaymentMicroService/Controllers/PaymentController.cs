using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentMicroService.ViewModels;
using PaymentMicroService.Interfaces;
using PaymentMicroService.Models;
using PaymentMicroService.Repositories;
using Stripe;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentMicroService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        // Process Payment - Returns ClientSecret
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentDto)
        {
            if (paymentDto == null || paymentDto.Amount <= 0)
            {
                return BadRequest("Invalid payment details.");
            }

            var clientSecret = await _paymentService.ProcessPaymentAsync(paymentDto);
            if (string.IsNullOrEmpty(clientSecret))
            {
                return BadRequest("Failed to create payment intent.");
            }

            return Ok(new { ClientSecret = clientSecret });
        }

        // Updates Payment Table
        [HttpPost("CompletePayment")]
        public async Task<IActionResult> CompletePayment([FromBody] CompletePaymentDTO completePaymentDto)
        {
            if (string.IsNullOrEmpty(completePaymentDto.PaymentId) || string.IsNullOrEmpty(completePaymentDto.PaymentIntentId))
            {
                return BadRequest("Invalid payment details.");
            }

            var result = await _paymentService.CompletePaymentAsync(completePaymentDto.PaymentId, completePaymentDto.PaymentIntentId);
            if (!result)
            {
                return NotFound("Payment record not found or update failed.");
            }

            return Ok(new { message = "Payment status updated successfully" });
        }
    }
}