using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("Match")]
    public class MatchesController : FootballClubBaseController<MatchesController>, IEntityController<Match>
    {
        public MatchesController(IStringLocalizer<MatchesController> localizer, ILogger<MatchesController> logger, 
            FootballClubDbContext footballClubDbContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Match GetEmptyEntity()
        {
            return new Match();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Match match)
        {
            ValidateEntity(match);

            FootballClubDbContext.Matches.Remove(match);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Matches.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var matches =
                from match in FootballClubDbContext.Matches.Skip(@from).Take(count).ToList()

                join tournament in FootballClubDbContext.Tournaments
                on match.TournamentId equals tournament.Id
                into tournaments

                from tournament in tournaments.DefaultIfEmpty()

                select match;

            return Ok(matches);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var matchesById =
                from match in FootballClubDbContext.Matches
                where match.Id == id

                select match;

            return Ok(matchesById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var matchesOptions =
                from matchesOption in FootballClubDbContext.Matches.Skip(@from).Take(count)

                select new
                {
                    Id = matchesOption.Id,
                    DisplayValue = $"{matchesOption.OurTeamName} - {matchesOption.EnemyTeamName}"
                };

            return Ok(matchesOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Match match)
        {
            ValidateEntity(match);

            FootballClubDbContext.Matches.Add(match);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Match match)
        {
            ValidateEntity(match);

            FootballClubDbContext.Matches.Update(match);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
