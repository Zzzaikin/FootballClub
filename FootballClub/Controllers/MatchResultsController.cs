﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchResultsController : FootballClubBaseController<MatchResultsController>
    {
        public MatchResultsController(IStringLocalizer<MatchResultsController> localizer, ILogger<MatchResultsController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
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