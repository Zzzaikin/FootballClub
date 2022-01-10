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
    [Route("Tournaments")]
    public class TournamentController : FootballClubBaseController<TournamentController>, IEntityController<Tournament>
    {
        public TournamentController(IStringLocalizer<TournamentController> localizer, ILogger<TournamentController> logger,
            FootballClubDbContext footballClubDbContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, configuration)
        { }

        public IActionResult DeleteEntity(Tournament entity)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetCountOfEntityRecords()
        {
            throw new NotImplementedException();
        }

        public Tournament GetEmptyEntity()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetEntityById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            var tournamentOptions =
                from tournamentOption in FootballClubDbContext.Tournaments.Skip(@from).Take(count)
                select new
                {
                    Id = tournamentOption.Id,
                    DisplayValue = $"{tournamentOption.Name} " +
                    $"{tournamentOption.StartDate.ToShortDateString()}-{tournamentOption.EndDate.ToShortDateString()}"
                };

            return Ok(tournamentOptions);
        }

        public IActionResult InsertEntity(Tournament entity)
        {
            throw new NotImplementedException();
        }

        public IActionResult UpdateEntity(Tournament entity)
        {
            throw new NotImplementedException();
        }
    }
}
