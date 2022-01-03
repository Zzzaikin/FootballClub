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
    [Route("Disqualification")]
    public class DisqualificationsController : FootballClubBaseController<DisqualificationsController>, IEntityController<Disqualification>
    {
        public DisqualificationsController(IStringLocalizer<DisqualificationsController> localizer, ILogger<DisqualificationsController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Disqualification GetEmptyEntity()
        {
            return new Disqualification();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Disqualification disqualification)
        {
            ValidateEntity(disqualification);

            FootballClubDbContext.Disqualifications.Remove(disqualification);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Disqualifications.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var disqualifications =
                 from disqualification in FootballClubDbContext.Disqualifications.Skip(@from).Take(count).ToList()

                 join person in FootballClubDbContext.Persons
                 on disqualification.PersonId equals person.Id
                 into persons

                 from person in persons.DefaultIfEmpty()

                 select disqualification;

            return Ok(disqualifications);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var disqualificationById =
                from disqualification in FootballClubDbContext.Disqualifications.ToList()
                where disqualification.Id == id

                select disqualification;

            return Ok(disqualificationById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var disqualificationOptions =
                from disqualificationOption in FootballClubDbContext.Disqualifications.Skip(@from).Take(count)

                select new
                {
                    Id = disqualificationOption.Id,
                    DisplayValue = disqualificationOption.Person.Name
                };

            return Ok(disqualificationOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Disqualification disqualification)
        {
            ValidateEntity(disqualification);

            FootballClubDbContext.Disqualifications.Add(disqualification);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Disqualification disqualification)
        {
            ValidateEntity(disqualification);

            FootballClubDbContext.Disqualifications.Update(disqualification);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
