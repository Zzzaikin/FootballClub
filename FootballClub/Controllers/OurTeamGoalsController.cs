using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("OurTeamGoal")]
    public class OurTeamGoalsController : FootballClubBaseController<OurTeamGoalsController>, IEntityController<OurTeamGoal>
    {
        public OurTeamGoalsController(IStringLocalizer<OurTeamGoalsController> localizer, ILogger<OurTeamGoalsController> logger,
            FootballClubDbContext footballClubDbContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, configuration)
        { }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(OurTeamGoal ourTeamGoal)
        {
            ValidateEntity(ourTeamGoal);

            FootballClubDbContext.OurTeamGoals.Remove(ourTeamGoal);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been deleted", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.OurTeamGoals.Count();
            return Ok(count);
        }

        [HttpGet("GetEmptyEntity")]
        public OurTeamGoal GetEmptyEntity()
        {
            return new OurTeamGoal();
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            ValidateIntervalParams(@from, count);

            var ourTeamGoals =
                from person in FootballClubDbContext.OurTeamGoals.Skip(@from).Take(count)

                select person;

            return Ok(ourTeamGoals);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            throw new NotImplementedException();
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(OurTeamGoal ourTeamGoal)
        {
            ValidateEntity(ourTeamGoal);

            FootballClubDbContext.OurTeamGoals.Add(ourTeamGoal);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(OurTeamGoal ourTeamGoal)
        {
            ValidateEntity(ourTeamGoal);

            FootballClubDbContext.OurTeamGoals.Update(ourTeamGoal);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        /// <summary>
        /// Возвращает голы нашей команды по идентификатор матча.
        /// </summary>
        /// <param name="matchId">Иентификатор матча</param>
        /// <returns>Статус выполнения запроса с голами нашей команды по идентификатору матча.</returns>
        [HttpGet("GetGoalsByMatchId")]
        public IActionResult GetGoalsByMatchId(Guid matchId)
        {
            ValidateId(matchId);

            var ourTeamGoalsByMatchId =
                from ourTeamGoal in FootballClubDbContext.OurTeamGoals
                where ourTeamGoal.MatchId == matchId

                select ourTeamGoal;

            return Ok(ourTeamGoalsByMatchId);
        }

        [HttpGet("GetTop5BestScorers")]
        public IActionResult GetTop5BestScorers(string tournamentName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrEmpty(tournamentName))
            {
                throw new ArgumentException("Tournament name can not be null or empty");
            }

            if (startDate > endDate)
            {
                throw new ArgumentException("Incorrect dates. Start date must be less end date");
            }

            var groupedOurTeamGoals =
                from ourTeamGoal in FootballClubDbContext.OurTeamGoals

                join match in FootballClubDbContext.Matches
                on ourTeamGoal.MatchId equals match.Id
                into matches

                from match in matches.DefaultIfEmpty()

                join tournament in FootballClubDbContext.Tournaments
                on match.TournamentId equals tournament.Id
                into tournaments

                from tournament in tournaments.DefaultIfEmpty()

                where 
                    (tournament.Name == tournamentName) &&
                    (tournament.StartDate == startDate) &&
                    (tournament.EndDate == endDate)

                group ourTeamGoal by ourTeamGoal.AuthorPlayerId
                into teamGoalsByAuthorPlayerId

                select new
                {
                    RecordCount = teamGoalsByAuthorPlayerId.Count(),
                    AuthorPlayerId = teamGoalsByAuthorPlayerId.Key
                };

            var bestScorers =
                from bestScorer in groupedOurTeamGoals.Take(5)

                join player in FootballClubDbContext.Players
                on bestScorer.AuthorPlayerId equals player.Id
                into bestScorersPlayers

                from bestScorersPlayer in bestScorersPlayers.DefaultIfEmpty()

                join person in FootballClubDbContext.Persons
                on bestScorersPlayer.PersonId equals person.Id
                into bestScorersPersons

                from bestScorersPerson in bestScorersPersons.DefaultIfEmpty()

                orderby bestScorer.RecordCount descending

                select new
                {
                    GoalsCount = bestScorer.RecordCount,
                    PlayerName = bestScorersPerson.Name
                };

            return Ok(bestScorers);
        }
    }
}