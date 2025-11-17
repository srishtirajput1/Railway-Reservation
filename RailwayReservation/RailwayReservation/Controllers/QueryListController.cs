using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryListController : ControllerBase
    {
        private IQueryList queryListRepo;

        public QueryListController(IQueryList _queryListRepo)
        {
            queryListRepo = _queryListRepo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQueryList([FromBody] QueryList queryList)
        {
            if (queryList == null)
            {
                return BadRequest();
            }

            await queryListRepo.AddQueryList(queryList);
            return CreatedAtAction(nameof(GetQueryListById), new { queryListId = queryList.QueryListId }, queryList);
        }

        [HttpGet("{queryListId}")]
        public async Task<IActionResult> GetQueryListById(string queryListId)
        {
            var queryList = await queryListRepo.GetQueryListById(queryListId);
            if (queryList == null)
            {
                return NotFound();
            }
            return Ok(queryList);
        }

        [HttpGet("query/{queryId}")]
        public async Task<IActionResult> GetQueryListsByQueryId(string queryId)
        {
            var queryLists = await queryListRepo.GetQueryListsByQueryId(queryId);
            if (!queryLists.Any())
            {
                return NotFound();
            }
            return Ok(queryLists);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQueryList([FromBody] QueryList queryList)
        {
            if (queryList == null)
            {
                return BadRequest();
            }

            await queryListRepo.UpdateQueryList(queryList);
            return NoContent();
        }

        [HttpDelete("{queryListId}")]
        public async Task<IActionResult> DeleteQueryList(string queryListId)
        {
            await queryListRepo.DeleteQueryList(queryListId);
            return NoContent();
        }
    }
}
