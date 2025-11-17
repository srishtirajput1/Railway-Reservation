using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrain _trainRepository;
        public TrainController(ITrain trainRepository)
        {
            _trainRepository = trainRepository;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllTrains()
        {
            var trains = await _trainRepository.GetAllTrainsAsync();
            if(!trains.Any())
            {
                return NotFound();
            }
            return Ok(trains);
        }

        // Get a specific train by ID
        [HttpGet("{trainId}")]
        public async Task<IActionResult> GetTrainById(string trainId)
        {
            var train = await _trainRepository.GetTrainByIdAsync(trainId);
            if (train == null)
            {
                return NotFound($"Train with ID {trainId} not found.");
            }

            return Ok(train);
        }


        // Add seats for a train
        [HttpPost("{trainId}/seats")]
        public async Task<IActionResult> AddSeats(string trainId, [FromBody] List<string> classIds)
        {
            var success = await _trainRepository.AddTrainSeatsAsync(trainId, classIds);
            if (success)
            {
                return Ok("Seats added successfully.");
            }

            return BadRequest("Failed to add seats.");
        }

        [HttpPost("Add")]
        public ActionResult<Train> AddTrain([FromBody] TrainVM trainVm)
        {
            if (ModelState.IsValid)
            {
                if (trainVm.SourceStation == trainVm.DestinationStation)
                {
                    return BadRequest(new { msg = "Source Station and Sestination Station must be Different" });
                }
                if (trainVm.AvailableGeneralSeat + trainVm.AvailableLadiesSeat != trainVm.TotalSeats)
                {
                    return BadRequest(new { msg = "Total of General and Ladies seats must be same as Total Seats" });
                }
                if (trainVm.JourneyEndDate > trainVm.JourneyStartDate)
                {
                    return BadRequest(new { msg = "Journey start date should be before journey end date" });
                }
                if (trainVm.JourneyStartDate <= DateTime.Now)
                {
                    return BadRequest(new { msg = "Journey start date should be a future date" });
                }

                Train train = _trainRepository.AddTrainDetails(trainVm);
                if (train == null)
                    return Conflict(new { msg = "Some error happens...Try Again" });

                return CreatedAtAction("AddTrain", train);
            }

            return ValidationProblem("Fill the data Properly...");
        }

        [HttpPut("{trainId}")]
        public IActionResult UpdateTrain(string trainId, [FromBody] TrainVM updatedTrain)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid train data.");
            }

            bool isUpdated = _trainRepository.UpdateTrain(trainId, updatedTrain);

            if (!isUpdated)
            {
                return NotFound(new { msg = "Train not found or update failed." });
            }

            return Ok(new { msg = "Train details updated successfully." });
        }

        [HttpDelete("{trainId}")]
        public IActionResult DeleteTrain(string trainId)
        {
            bool isDeleted = _trainRepository.DeleteTrain(trainId);

            if (!isDeleted)
            {
                return NotFound(new { msg = "Train not found or deletion failed." });
            }

            return Ok(new { msg = "Train deleted successfully." });
        }


    }
}