using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private IQuery queryRepository;

        public QueryController(IQuery _queryRepository)
        {
            queryRepository = _queryRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQuery([FromBody] Query query)
        {
            await queryRepository.AddQuery(query);
            return CreatedAtAction(nameof(GetQueryById), new { queryId = query.QueryId }, query);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllQueries()
        {
            var queries = await queryRepository.GetAllQueries();
            if (!queries.Any())
            {
                return NotFound("No queries found");
            }

            return Ok(queries);
        }

        [HttpGet("{queryId}")]
        public async Task<IActionResult> GetQueryById(string queryId)
        {
            var query = await queryRepository.GetQueryById(queryId);
            if (query == null)
            {
                return NotFound();
            }
            return Ok(query);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetQueriesByKeyword([FromQuery] string keyword)
        {
            var queries = await queryRepository.GetQueriesByKeyword(keyword);
            if (!queries.Any())
            {
                return NotFound();
            }

            return Ok(queries);
        }
    }
}
