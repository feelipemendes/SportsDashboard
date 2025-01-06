using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsDashboard.Services;

namespace SportsDashboard.Controllers
{
    [ApiController]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        [Route("recent")]
        public async Task<IActionResult> Recent([FromQuery] int pageSize = 10, int page = 1)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var status = "FINISHED";

            var competitions = new List<int>();

            competitions.AddRange([2013, 2014, 2019, 2002, 2003, 2021, 2017]);

            var data = await _matchService.GetMatchesForCompetitions(competitions, status);

            if (!data.Any())
            {
                return NotFound("No matches found.");
            }


            var totalItems = data.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (page > totalPages)
            {
                return NotFound($"Page {page} exceeds total pages {totalPages}.");
            }

            var paginatedData = data
                .Skip((page - 1) * pageSize) 
                .Take(pageSize)             
                .ToList();

            var response = new
            {
                currentPage = page,
                pageSize,
                totalItems,
                totalPages,
                items = paginatedData
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("upcoming")]
        public async Task<IActionResult> Upcoming()
        {
            var status = "SCHEDULED";

            var competitions = new List<int>();

            competitions.AddRange([2013, 2014, 2019, 2002, 2003, 2021, 2017]);

            var data = await _matchService.GetMatchesForCompetitions(competitions, status);

            return Ok(data);
        }
    }
}
