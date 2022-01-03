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
    [Route("PlayerManager")]
    public class PlayerManagersController : FootballClubBaseController<PlayerManagersController>, IEntityController<PlayerManager>
    {
        public PlayerManagersController(IStringLocalizer<PlayerManagersController> localizer, ILogger<PlayerManagersController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public PlayerManager GetEmptyEntity()
        {
            return new PlayerManager();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(PlayerManager playerManager)
        {
            ValidateEntity(playerManager);

            FootballClubDbContext.PlayerManagers.Remove(playerManager);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.PlayerManagers.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var playerManagers =
                 from playerManager in FootballClubDbContext.PlayerManagers.Skip(@from).Take(count).ToList()

                 join person in FootballClubDbContext.Persons
                 on playerManager.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select playerManager;

            return Ok(playerManagers);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var playerManagerById =
                from playerManager in FootballClubDbContext.PlayerManagers.ToList()
                where playerManager.Id == id

                join person in FootballClubDbContext.Persons
                on playerManager.Id equals person.Id
                into persons

                from playerManagerPerson in persons.DefaultIfEmpty()

                select playerManager;

            return Ok(playerManagerById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var playerManagersOptions =
                from playerManagersOption in FootballClubDbContext.PlayerManagers.Skip(@from).Take(count).ToList()

                join person in FootballClubDbContext.Persons
                on playerManagersOption.PersonId equals person.Id
                into persons

                from playerPerson in persons.DefaultIfEmpty()

                select new
                {
                    Id = playerManagersOption.Id,
                    DisplayValue = playerPerson.Name
                };

            return Ok(playerManagersOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(PlayerManager playerManager)
        {
            ValidateEntity(playerManager);

            FootballClubDbContext.PlayerManagers.Add(playerManager);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(PlayerManager playerManager)
        {
            ValidateEntity(playerManager);

            FootballClubDbContext.PlayerManagers.Update(playerManager);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
