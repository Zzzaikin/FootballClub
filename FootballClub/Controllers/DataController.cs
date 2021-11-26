using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с базой данных.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger<DataController> _logger;

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly FootballClubDbContext _footballClubDbContext;

        /// <summary>
        /// Инициализирует контекст базы данных и логгер.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="footballClubDbContext">Контекст базы данных</param>
        public DataController(ILogger<DataController> logger, FootballClubDbContext footballClubDbContext)
        {
            _logger = logger;
            _footballClubDbContext = footballClubDbContext;
        }

        /// <summary>
        /// Возвращает игроков на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Игроков со статусом запроса</returns>
        [HttpGet("GetPlayersForSection")]
        public IActionResult GetPlayersForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var players =
                 from player in _footballClubDbContext.Players.Skip(@from).Take(count).ToList()

                 join person in _footballClubDbContext.Persons
                 on player.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select player;

            return Ok(players);
        }

        /// <summary>
        /// Возвращает тренеров на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Тренеров со статусом запроса</returns>
        [HttpGet("GetCoachesForSection")]
        public IActionResult GetCoachesForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var coaches =
                 from coach in _footballClubDbContext.Coaches.Skip(@from).Take(count).ToList()

                 join person in _footballClubDbContext.Persons
                 on coach.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select coach;

            return Ok(coaches);
        }

        /// <summary>
        /// Возвращает матчи на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Матчи со статусом запроса</returns>
        [HttpGet("GetMatchesForSection")]
        public IActionResult GetMatchesForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var matches =
                from match in _footballClubDbContext.Matches.Skip(@from).Take(count).ToList()

                join matchResult in _footballClubDbContext.MatchResults
                on match.MatchResultId equals matchResult.Id
                into matchResults

                from matchResult in matchResults.DefaultIfEmpty()

                select match;

            return Ok(matches);
        }

        /// <summary>
        /// Возвращает дисквалификации на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Дисквалификации со статусом запроса</returns>
        [HttpGet("GetDisqualificationsForSection")]
        public IActionResult GetDisqualificationsForSection(int from = 0, int count = 9)
        {
            var disqualifications =
                from disqualification in _footballClubDbContext.Disqualifications.Skip(@from).Take(count).ToList()

                join player in _footballClubDbContext.Players
                on disqualification.PlayerId equals player.Id
                into players

                from player in players.DefaultIfEmpty()

                join person in _footballClubDbContext.Persons
                on player.PersonId equals person.Id
                into persons

                from person in persons.DefaultIfEmpty()

                select disqualification;

            return Ok(disqualifications);
        }

        /// <summary>
        /// Возвращает менеджеров игроков на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Менеджеров игроков со статусом запроса</returns>
        [HttpGet("GetPlayerManagersForSection")]
        public IActionResult GetPlayerManagersForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var playerManagers =
                 from playerManager in _footballClubDbContext.PlayerManagers.Skip(@from).Take(count).ToList()

                 join person in _footballClubDbContext.Persons
                 on playerManager.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 select playerManager;

            return Ok(playerManagers);
        }

        /// <summary>
        /// Возвращает взыскания с игроков на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Взыскания с игроков со статусом запроса</returns>
        [HttpGet("GetEmployeeRecoveriesForSection")]
        public IActionResult GetEmployeeRecoveriesForSection(int from = 0, int count = 9)
        {
            ValidateIntervalParams(from, count);

            var employeeRecoveries =
                 from employeeRecovery in _footballClubDbContext.EmployeeRecoveries.Skip(@from).Take(count).ToList()

                 join person in _footballClubDbContext.Persons
                 on employeeRecovery.PersonId equals person.Id
                 into persons

                 from playerPerson in persons.DefaultIfEmpty()

                 join recoveryReason in _footballClubDbContext.RecoveryReasons
                 on employeeRecovery.RecoveryReasonId equals recoveryReason.Id
                 into recoveryReasons

                 from recoveryReason in recoveryReasons.DefaultIfEmpty()

                 select employeeRecovery;

            return Ok(employeeRecoveries);
        }

        /// <summary>
        /// Возвращает опции для графика суммы взысканий.
        /// </summary>
        /// <returns>Опции с результатом запроса.</returns>
        [HttpGet("GetGraphicOfSumOptions")]
        public IActionResult GetGraphicOfSumOptions()
        {
            return Ok();
        }

        /// <summary>
        /// Добавляет человека.
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>Статус запроса.</returns>
        [HttpPost("AddPerson")]
        public IActionResult AddPerson(Person person)
        {
            _footballClubDbContext.Persons.Add(person);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }


        /// <summary>
        /// Добавляет игрока.
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddPlayer")]
        public IActionResult AddPlayer(Player player)
        {
            _footballClubDbContext.Players.Add(player);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет тренера.
        /// </summary>
        /// <param name="coach">Тренер</param>
        /// <returns>Статцс запроса</returns>
        [HttpPost("AddCoach")]
        public IActionResult AddCoach(Coach coach)
        {
            _footballClubDbContext.Coaches.Add(coach);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет контракт.
        /// </summary>
        /// <param name="contract">Контракт</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddContract")]
        public IActionResult AddContract(Contract contract)
        {
            _footballClubDbContext.Contracts.Add(contract);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет менеджера игрока.
        /// </summary>
        /// <param name="playerManager">Менеджер игрока</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddPlayerManager")]
        public IActionResult AddPlayerManager(PlayerManager playerManager)
        {
            _footballClubDbContext.PlayerManagers.Add(playerManager);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет взыскание с игрока.
        /// </summary>
        /// <param name="employeeRecovery">Взыскание с игрока</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddEmployeeRecovery")]
        public IActionResult AddEmployeeRecovery(EmployeeRecovery employeeRecovery)
        {
            _footballClubDbContext.EmployeeRecoveries.Add(employeeRecovery);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет матч.
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddMatch")]
        public IActionResult AddMatch(Match match)
        {
            _footballClubDbContext.Matches.Add(match);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Добавляет дисквалификацию.
        /// </summary>
        /// <param name="disqualification">Дискавалификация</param>
        /// <returns>Статус запроса</returns>
        [HttpPost("AddDisqualification")]
        public IActionResult AddDisqualification(Disqualification disqualification)
        {
            _footballClubDbContext.Disqualifications.Add(disqualification);
            _footballClubDbContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Удаляет запись по названию сущности и идентификатору.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Статус выполнения запроса.</returns>
        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity (string entityName, Guid id)
        {
            ValidateEntityNameAndId(entityName, id);

            _footballClubDbContext.Database.ExecuteSqlRaw("DELETE FROM footballclub." + entityName +" WHERE Id = {0}", id);

            return Ok();
        }

        /// <summary>
        /// Валидирует название сущности и её идентификатор.
        /// </summary>
        /// <param name="entityName">Название сущности./param>
        /// <param name="id">Идентификатор сущности.</param>
        private void ValidateEntityNameAndId(string entityName, Guid id)
        {
            if (string.IsNullOrEmpty(entityName))
            {
                throw new ArgumentException($"{nameof(entityName)} can not be null or empty.");
            }

            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} can not be empty.");
            }
        }

        /// <summary>
        /// Валидирует интервальные параметры.
        /// </summary>
        /// <param name="from">От</param>
        /// <param name="count">Количество.</param>
        private void ValidateIntervalParams(int from, int count)
        {
            if (from < 0)
            {
                throw new Exception($"Parametr {nameof(from)} can not be less than zero.");
            }

            if (count < 0)
            {
                throw new Exception($"Parametr {nameof(count)} can not be less than zero.");
            }
        }
    }
}
