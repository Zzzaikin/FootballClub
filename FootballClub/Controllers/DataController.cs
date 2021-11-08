using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost("AddPerson")]
        public IActionResult AddPerson(Person person)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.Persons.Add(person);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddCoach")]
        public IActionResult AddCoach(Coach coach)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.Coaches.Add(coach);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddContract")]
        public IActionResult AddContract(Contract contract)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.Contracts.Add(contract);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddPlayerManager")]
        public IActionResult AddPlayerManager(PlayerManager playerManager)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.PlayerManagers.Add(playerManager);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddEmployeeRecovery")]
        public IActionResult AddEmployeeRecovery(EmployeeRecovery employeeRecovery)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.EmployeeRecoveries.Add(employeeRecovery);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddMatch")]
        public IActionResult AddMatch(Match match)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.Matches.Add(match);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("AddDisqualification")]
        public IActionResult AddDisqualification(Disqualification disqualification)
        {
            using (_footballClubDbContext)
            {
                _footballClubDbContext.Disqualifications.Add(disqualification);
                _footballClubDbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
