using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Models;

[ApiController]
[Route("api/[controller]")]
public class TrainClassController : ControllerBase
{
    private readonly TrainClassRepository _trainClassRepository;

    public TrainClassController(TrainClassRepository trainClassRepository)
    {
        _trainClassRepository = trainClassRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTrainClasses()
    {
        var trainClasses = await _trainClassRepository.GetAllTrainClassesAsync();
        return Ok(trainClasses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrainClassById(int id)
    {
        var trainClass = await _trainClassRepository.GetTrainClassByIdAsync(id);

        if (trainClass == null)
        {
            return NotFound($"TrainClass with ID {id} not found.");
        }

        return Ok(trainClass);
    }

    // POST api/trainclass
    [HttpPost]
    public async Task<IActionResult> CreateTrainClass([FromBody] TrainClass trainClass)
    {
        if (trainClass == null)
        {
            return BadRequest("TrainClass data is required.");
        }

        var createdTrainClass = await _trainClassRepository.CreateTrainClassAsync(trainClass);
        return CreatedAtAction(nameof(GetTrainClassById), new { id = createdTrainClass.TrainClassId }, createdTrainClass);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTrainClass(int id, [FromBody] TrainClass updatedTrainClass)
    {
        if (updatedTrainClass == null)
        {
            return BadRequest("TrainClass data is required.");
        }

        var trainClass = await _trainClassRepository.UpdateTrainClassAsync(id, updatedTrainClass);

        if (trainClass == null)
        {
            return NotFound($"TrainClass with ID {id} not found.");
        }

        return Ok(trainClass);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrainClass(int id)
    {
        var result = await _trainClassRepository.DeleteTrainClassAsync(id);

        if (!result)
        {
            return NotFound($"TrainClass with ID {id} not found.");
        }

        return NoContent(); // Successfully deleted
    }
}
