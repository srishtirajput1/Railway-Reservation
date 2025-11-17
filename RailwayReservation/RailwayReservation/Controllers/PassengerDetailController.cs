using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerDetailController : ControllerBase
    {
        private readonly IPassengerDetail _passengerDetailRepository;

        public PassengerDetailController(IPassengerDetail passengerDetailRepository)
        {
            _passengerDetailRepository = passengerDetailRepository;
        }

        // GET: api/PassengerDetail
        [HttpGet]
        public async Task<IActionResult> GetAllPassengerDetails()
        {
            var passengerDetails = await _passengerDetailRepository.GetAllPassengerDetailsAsync();
            if (passengerDetails == null)
            {
                return NotFound();
            }
            return Ok(passengerDetails);
        }

        // GET: api/PassengerDetail/{passengerId}
        [HttpGet("{passengerId}")]
        public async Task<IActionResult> GetPassengerDetail(string passengerId)
        {
            var passengerDetail = await _passengerDetailRepository.GetPassengerDetailByIdAsync(passengerId);
            if (passengerDetail == null)
            {
                return NotFound();
            }

            return Ok(passengerDetail);
        }

        // POST: api/PassengerDetail
        [HttpPost("add")]
        public IActionResult AddPassengers([FromBody] List<PassengerDTO> passengers, [FromQuery] string userId)
        {
            if (passengers == null || passengers.Count == 0)
            {
                return BadRequest("Passenger list cannot be empty.");
            }

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId cannot be null or empty.");
            }


            var result = _passengerDetailRepository.AddPassenger(passengers, userId);

            //if (result == null)
            //{
            //    return NotFound("User not found.");
            //}

            //if (result.Count == 0)
            //{
            //    return StatusCode(500, "An error occurred while saving passenger details.");
            //}

            return Ok("successfully added.");
        }

        

        

        // PUT: api/PassengerDetail/{passengerId}
        [HttpPut("{passengerId}")]
        public async Task<IActionResult> UpdatePassengerDetail(string passengerId, [FromBody] PassengerDetail passengerDetail)
        {
            if (passengerDetail == null || passengerDetail.PassengerId != passengerId)
            {
                return BadRequest("PassengerId mismatch or invalid data.");
            }

            if (passengerDetail.SeatNumber < 1 || passengerDetail.SeatNumber > 6)
            {
                return BadRequest("Seat number must be between 1 and 6.");
            }

            var updated = await _passengerDetailRepository.UpdatePassengerDetailAsync(passengerDetail);
            if (!updated)
            {
                return NotFound();
            }

            return Ok(passengerDetail);
        }

        // DELETE: api/PassengerDetail/{passengerId}
        [HttpDelete("{passengerId}")]
        public async Task<IActionResult> DeletePassengerDetail(string passengerId)
        {
            var deleted = await _passengerDetailRepository.DeletePassengerDetailAsync(passengerId);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok("Successfully Deleted Passenger Details");
        }
    }

}