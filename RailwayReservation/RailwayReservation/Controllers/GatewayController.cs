using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RailwayReservation.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RailwayReservation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GatewayController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public GatewayController(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }

    [HttpPost("ProcessPayment")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentDto)
    {
        var client = _clientFactory.CreateClient();
        var paymentUrl = _configuration["ServiceUrls:PaymentMicroService"];
        var jsonData = JsonConvert.SerializeObject(paymentDto);
        var content= new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{paymentUrl}/api/Payment/ProcessPayment",content);

        if (response.IsSuccessStatusCode)
        {
            var resp = await response.Content.ReadAsStringAsync();
            return Ok(resp);
        }
        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }

    [HttpPost("CompletePayment")]
    public async Task<IActionResult> CompletePayment([FromBody] CompletePaymentDTO completePaymentDto)
    {
        var client = _clientFactory.CreateClient();
        var paymentUrl = _configuration["ServiceUrls:PaymentMicroService"];
        var jsonData = JsonConvert.SerializeObject(completePaymentDto);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"{paymentUrl}/api/Payment/CompletePayment", content);

        if (response.IsSuccessStatusCode)
        {
            var resp = await response.Content.ReadAsStringAsync();
            return Ok(resp);
        }
        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }


}
