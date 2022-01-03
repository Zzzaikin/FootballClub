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
    [Route("EmployeeRecovery")]
    public class EmployeeRecoveriesController : FootballClubBaseController<EmployeeRecoveriesController>, IEntityController<EmployeeRecovery>
    {
        public EmployeeRecoveriesController(IStringLocalizer<EmployeeRecoveriesController> localizer, ILogger<EmployeeRecoveriesController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public EmployeeRecovery GetEmptyEntity()
        {
            return new EmployeeRecovery();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(EmployeeRecovery employeeRecovery)
        {
            ValidateEntity(employeeRecovery);

            FootballClubDbContext.EmployeeRecoveries.Remove(employeeRecovery);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.EmployeeRecoveries.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var employeeRecoveries =
                 from employeeRecovery in FootballClubDbContext.EmployeeRecoveries.Skip(@from).Take(count).ToList()

                 join person in FootballClubDbContext.Persons
                 on employeeRecovery.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 join recoveryReason in FootballClubDbContext.RecoveryReasons
                 on employeeRecovery.RecoveryReasonId equals recoveryReason.Id
                 into recoveryReasons

                 from recoveryReason in recoveryReasons.DefaultIfEmpty()

                 select employeeRecovery;

            return Ok(employeeRecoveries);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var employeeRecoveryById =
                from employeeRecovery in FootballClubDbContext.EmployeeRecoveries
                where employeeRecovery.Id == id

                select employeeRecovery;

            return Ok(employeeRecoveryById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var employeeRecoveryOptions =
                from employeeRecoveryOption in FootballClubDbContext.EmployeeRecoveries.Skip(@from).Take(count)

                select new
                {
                    Id = employeeRecoveryOption.Id,
                    DisplayValue = $"{employeeRecoveryOption.Person.Name} - ${employeeRecoveryOption.RecoveryReason.DisplayName} - " +
                        $"{employeeRecoveryOption.Sum} {Localizer["Currensy"].Value}"
                };

            return Ok(employeeRecoveryOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(EmployeeRecovery employeeRecovery)
        {
            ValidateEntity(employeeRecovery);

            FootballClubDbContext.EmployeeRecoveries.Add(employeeRecovery);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(EmployeeRecovery employeeRecovery)
        {
            ValidateEntity(employeeRecovery);

            FootballClubDbContext.EmployeeRecoveries.Update(employeeRecovery);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
