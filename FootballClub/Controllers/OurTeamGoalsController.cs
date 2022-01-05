using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    public class OurTeamGoalsController : FootballClubBaseController<OurTeamGoalsController>, IEntityController<OurTeamGoal>
    {
        public OurTeamGoalsController(IStringLocalizer<OurTeamGoalsController> localizer, ILogger<OurTeamGoalsController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
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
            var ourTeamGoalOptions =
                from ourTeamGoalOption in FootballClubDbContext.OurTeamGoals.ToList()

                join player in FootballClubDbContext.Players
                on ourTeamGoalOption.Author equals player.Id
                into players

                from goalAuthor in players.DefaultIfEmpty()

                join person in FootballClubDbContext.Persons
                on goalAuthor.Id equals person.Id
                into goalPersons

                from goalPerson in goalPersons.DefaultIfEmpty()

                select new
                {
                    Id = ourTeamGoalOption.Id,
                    DisplayName = $"{Localizer["Score_A_Goal"].Value} {goalPerson.Name}"
                };

            return Ok(ourTeamGoalOptions);
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
    }
}
