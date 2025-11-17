using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Models;
using RailwayReservation.Interfaces;
using RailwayReservation.ViewModels;

[Route("api/[controller]")]
[ApiController]
public class SupportController : ControllerBase
{
    private readonly ISupport _supportRepository;

    public SupportController(ISupport supportRepository)
    {
        _supportRepository = supportRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSupports()
    {
        try
        {
            var supports = await _supportRepository.GetAllSupportsAsync();
            return Ok(supports);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving support records.", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupportById(string id)
    {
        try
        {
            var support = await _supportRepository.GetSupportByIdAsync(id);
            if (support == null)
            {
                return NotFound(new { message = "Support record not found." });
            }
            return Ok(support);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the support record.", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddSupport([FromBody] SupportRequest supportDto)
    {
        try
        {
            var support = new Support
            {
                SupportId = Guid.NewGuid().ToString(),
                UserId = supportDto.UserId,
                QueryListId = supportDto.QueryListId,
                QueryText = supportDto.QueryText,
                Status = supportDto.Status
            };

            await _supportRepository.AddSupportAsync(support);
            return CreatedAtAction(nameof(GetSupportById), new { id = support.SupportId }, support);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the support record.", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupport(string id, [FromBody] SupportRequest supportDto)
    {
        try
        {
            var existingSupport = await _supportRepository.GetSupportByIdAsync(id);
            if (existingSupport == null)
            {
                return NotFound(new { message = "Support record not found." });
            }

            existingSupport.QueryListId = supportDto.QueryListId;
            existingSupport.QueryText = supportDto.QueryText;
            existingSupport.Status = supportDto.Status;

            await _supportRepository.UpdateSupportAsync(existingSupport);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating the support record.", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupport(string id)
    {
        try
        {
            var support = await _supportRepository.GetSupportByIdAsync(id);
            if (support == null)
            {
                return NotFound(new { message = "Support record not found." });
            }

            await _supportRepository.DeleteSupportAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the support record.", error = ex.Message });
        }
    }
}
