using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClass _classRepository;

        public ClassController(IClass classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            try
            {
                var classes = await _classRepository.GetAll();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving class records.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(string id)
        {
            try
            {
                var classEntity = await _classRepository.GetById(id);
                if (classEntity == null)
                {
                    return NotFound(new { message = "Class not found." });
                }
                return Ok(classEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the class.", error = ex.Message });
            }
        }

        [HttpGet("class-name")]
        public async Task<IActionResult> SearchByClassName([FromQuery] string className)
        {
            try
            {
                var classes = await _classRepository.SearchByClassName(className);
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching for classes.", error = ex.Message });
            }
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetByClassType([FromQuery] string classType)
        {
            try
            {
                var classes = await _classRepository.GetByClassType(classType);
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while filtering classes by type.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody] Class classEntity)
        {
            try
            {
                var createdClass = await _classRepository.AddClass(classEntity);
                return CreatedAtAction(nameof(GetClassById), new { id = createdClass.ClassId }, createdClass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the class.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(string id, [FromBody] Class classEntity)
        {
            try
            {
                var existingClass = await _classRepository.GetById(id);
                if (existingClass == null)
                {
                    return NotFound(new { message = "Class not found." });
                }

                await _classRepository.UpdateClass(id, classEntity);
                return Ok("Successfully added the class");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the class.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            try
            {
                var deleted = await _classRepository.DeleteClass(id);
                if (!deleted)
                {
                    return NotFound(new { message = "Class not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the class.", error = ex.Message });
            }
        }
    }
}
