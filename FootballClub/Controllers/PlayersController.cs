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
    [Route("Player")]
    public class PlayersController : FootballClubBaseController<PlayersController>, IEntityController<Player>
    {
        public PlayersController(IStringLocalizer<PlayersController> localizer, ILogger<PlayersController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration) 
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Player GetEmptyEntity()
        {
            return new Player();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Player player)
        {
            ValidateEntity(player);

            FootballClubDbContext.Players.Remove(player);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Players.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var players =
                 from player in FootballClubDbContext.Players.Skip(@from).Take(count).ToList()

                 join person in FootballClubDbContext.Persons
                 on player.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select player;

            return Ok(players);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var playerById =
                from player in FootballClubDbContext.Players.ToList()
                where player.Id == id

                join person in FootballClubDbContext.Persons
                on player.PersonId equals person.Id
                into persons

                from playerPerson in persons.DefaultIfEmpty()

                select player;

            return Ok(playerById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var playersOptions =
                from playerOption in FootballClubDbContext.Players.Skip(@from).Take(count).ToList()

                join person in FootballClubDbContext.Persons
                on playerOption.PersonId equals person.Id
                into persons

                from playerPerson in persons.DefaultIfEmpty()

                select new
                {
                    Id = playerOption.Id,
                    DisplayValue = playerPerson.Name
                };

            return Ok(playersOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Player player)
        {
            ValidateEntity(player);

            FootballClubDbContext.Players.Add(player);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Player player)
        {
            ValidateEntity(player);

            FootballClubDbContext.Players.Update(player);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
