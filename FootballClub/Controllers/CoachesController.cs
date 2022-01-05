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
    [Route("Coach")]
    public class CoachesController : FootballClubBaseController<CoachesController>, IEntityController<Coach>
    {
        public CoachesController(IStringLocalizer<CoachesController> localizer, ILogger<CoachesController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Coach GetEmptyEntity()
        {
            return new Coach();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Coach coach)
        {
            ValidateEntity(coach);

            FootballClubDbContext.Coaches.Remove(coach);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Coaches.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var coaches =
                 from coach in FootballClubDbContext.Coaches.Skip(@from).Take(count).ToList()

                 join person in FootballClubDbContext.Persons
                 on coach.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select coach;

            return Ok(coaches);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var coachById =
                from coach in FootballClubDbContext.Coaches.ToList()
                where coach.Id == id

                join person in FootballClubDbContext.Persons
                on coach.Id equals person.Id
                into persons

                from coachPerson in persons.DefaultIfEmpty()

                select coach;

            return Ok(coachById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var coachesOptions =
                from coachesOption in FootballClubDbContext.Coaches.Skip(@from).Take(count)

                select new
                {
                    Id = coachesOption.Id,
                    DisplayValue = coachesOption.Person.Name
                };

            return Ok(coachesOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Coach coach)
        {
            ValidateEntity(coach);

            FootballClubDbContext.Coaches.Add(coach);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Coach coach)
        {
            ValidateEntity(coach);

            FootballClubDbContext.Coaches.Update(coach);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
