using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        private readonly FootballClubDbContext _footballClubDbContext;

        public DataController(ILogger<DataController> logger, FootballClubDbContext footballClubDbContext)
        {
            _logger = logger;
            _footballClubDbContext = footballClubDbContext;
        }

        [HttpGet("GetPlayersForSection")]
        public IActionResult GetPlayersForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var players =
                 from player in _footballClubDbContext.Players.ToList().Skip(@from).Take(count)

                 join person in _footballClubDbContext.Persons.ToList()
                 on player.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select player;

            return Ok(players);
        }

        [HttpGet("GetCoachesForSection")]
        public IActionResult GetCoachesForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var coaches =
                 from coach in _footballClubDbContext.Coaches.ToList().Skip(@from).Take(count)

                 join person in _footballClubDbContext.Persons.ToList()
                 on coach.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select coach;

            return Ok(coaches);
        }

        [HttpGet("GetPlayerManagersForSection")]
        public IActionResult GetPlayerManagersForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var playerManagers =
                 from playerManager in _footballClubDbContext.PlayerManagers.ToList().Skip(@from).Take(count)

                 join person in _footballClubDbContext.Persons.ToList()
                 on playerManager.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select playerManager;

            return Ok(playerManagers);
        }

        [HttpGet("GetEmployeeRecoveriesForSection")]
        public IActionResult GetEmployeeRecoveriesForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var employeeRecoveries =
                 from employeeRecovery in _footballClubDbContext.EmployeeRecoveries.ToList().Skip(@from).Take(count)

                 join person in _footballClubDbContext.Persons.ToList()
                 on employeeRecovery.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 join recoveryReason in _footballClubDbContext.RecoveryReasons.ToList()
                 on employeeRecovery.RecoveryReasonId equals recoveryReason.Id
                 into recoveryReasons

                 from recoveryReason in recoveryReasons.DefaultIfEmpty()

                 select employeeRecovery;

            return Ok(employeeRecoveries);
        }

        [HttpPost("AddPerson")]
        public IActionResult AddPerson(Person person)
        {
            _footballClubDbContext.Persons.Add(person);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddPlayer")]
        public IActionResult AddPlayer(Player player)
        {
            _footballClubDbContext.Players.Add(player);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddCoach")]
        public IActionResult AddCoach(Coach coach)
        {
            _footballClubDbContext.Coaches.Add(coach);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddContract")]
        public IActionResult AddContract(Contract contract)
        {
            _footballClubDbContext.Contracts.Add(contract);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddPlayerManager")]
        public IActionResult AddPlayerManager(PlayerManager playerManager)
        {
            _footballClubDbContext.PlayerManagers.Add(playerManager);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddEmployeeRecovery")]
        public IActionResult AddEmployeeRecovery(EmployeeRecovery employeeRecovery)
        {
            _footballClubDbContext.EmployeeRecoveries.Add(employeeRecovery);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddMatch")]
        public IActionResult AddMatch(Match match)
        {
            _footballClubDbContext.Matches.Add(match);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("AddDisqualification")]
        public IActionResult AddDisqualification(Disqualification disqualification)
        {
            _footballClubDbContext.Disqualifications.Add(disqualification);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        private void ValidateIntervalParams(int from, int count)
        {
            if (from < 0)
            {
                throw new Exception($"Parametr {nameof(from)} can not be less than zero");
            }

            if (count < 0)
            {
                throw new Exception($"Parametr {nameof(count)} can not be less than zero");
            }
        }
    }
}
