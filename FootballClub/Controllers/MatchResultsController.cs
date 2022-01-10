using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("MatchResult")]
    public class MatchResultsController : FootballClubBaseController<MatchResultsController>
    {
        public MatchResultsController(IStringLocalizer<MatchResultsController> localizer, ILogger<MatchResultsController> logger, 
            FootballClubDbContext footballClubDbContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, configuration)
        { }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var matchResultOptions =
                from matchResultOption in FootballClubDbContext.MatchResults.Skip(@from).Take(count)

                select new
                {
                    Id = matchResultOption.Id,
                    DisplayValue = matchResultOption.DisplayName
                };

            return Ok(matchResultOptions);
        }
    }
}