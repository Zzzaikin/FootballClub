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
    [Route("Person")]
    public class PersonsController : FootballClubBaseController<PersonsController>, IEntityController<Person>
    {
        public PersonsController(IStringLocalizer<PersonsController> localizer, ILogger<PersonsController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Person GetEmptyEntity()
        {
            return new Person();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Person person)
        {
            ValidateEntity(person);

            FootballClubDbContext.Persons.Remove(person);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Persons.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 0)
        {
            ValidateIntervalParams(@from, count);

            var persons =
                from person in FootballClubDbContext.Persons.Skip(@from).Take(count)

                select person;

            return Ok(persons);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var personById =
                from person in FootballClubDbContext.Persons.ToList()
                where person.Id == id

                select person;

            return Ok(personById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var personsOptions =
                from personOption in FootballClubDbContext.Persons.Skip(@from).Take(count)

                select new
                {
                    Id = personOption.Id,
                    DisplayValue = personOption.Name
                };

            return Ok(personsOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Person person)
        {
            ValidateEntity(person);

            FootballClubDbContext.Persons.Add(person);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Person person)
        {
            ValidateEntity(person);

            FootballClubDbContext.Persons.Update(person);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
