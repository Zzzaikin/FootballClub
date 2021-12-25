using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// Локализатор.
        /// </summary>
        private readonly IStringLocalizer<DataController> _localizer;

        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger<DataController> _logger;

        /// <summary>
        /// Контекст базы данных футбольного клуба.
        /// </summary>
        private readonly FootballClubDbContext _footballClubDbContext;

        /// <summary>
        /// Контекст базы данных схемы объектов.
        /// </summary>
        private readonly InformationSchemaContext _informationSchemaContext;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Инициализирует контексты базы данных, логгер, конфигурацию и локализатор.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="footballClubDbContext">Контекст базы данных футбольного клуба</param>
        /// <param name="localizer">Локализатор.</param>
        /// <param name="informationSchemaContext">Контекст базы данных схемы объектов</param>
        /// <param name="configuration">Конфигурация</param>
        public DataController(IStringLocalizer<DataController> localizer, ILogger<DataController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext,
            IConfiguration configuration)
        {
            _localizer = localizer;
            _logger = logger;
            _footballClubDbContext = footballClubDbContext;
            _informationSchemaContext = informationSchemaContext;
            _configuration = configuration;
        }

        /// <summary>
        /// Обновляет игрока и персону.
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <param name="person">Персона</param>
        /// <returns>Результат выполнения запроса.</returns>
        [HttpPost("UpdatePlayers")]
        public IActionResult UpdatePlayer(Player player)
        {
            ValidateEntity(player);

            _footballClubDbContext.Players.Update(player);
            var countOfEditRecords = _footballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated", statusCode: 500);
        }

        /// <summary>
        /// Возвращает человека по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Статус выполнения запроса с человеком, найденным по идентификатору.</returns>
        [HttpGet("GetPersonsById")]
        public IActionResult GetPersonById(Guid id)
        {
            ValidateId(id);

            var personById =
                from person in _footballClubDbContext.Persons.ToList()
                where person.Id == id

                select person;

            return Ok(personById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает игрока по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Результат выполнения запроса с игроком.</returns>
        [HttpGet("GetPlayersById")]
        public IActionResult GetPlayerById(Guid id)
        {
            ValidateId(id);

            var playerById =
                from player in _footballClubDbContext.Players.ToList()
                where player.Id == id

                join person in _footballClubDbContext.Persons
                on player.PersonId equals person.Id
                into persons

                from playerPerson in persons.DefaultIfEmpty()

                select player;

            return Ok(playerById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает тренера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Результат выполнения запроса с тренером.</returns>
        [HttpGet("GetCoachesById")]
        public IActionResult GetCoachById(Guid id)
        {
            ValidateId(id);

            var coacheById =
                from coach in _footballClubDbContext.Coaches.ToList()
                where coach.Id == id

                join person in _footballClubDbContext.Persons
                on coach.Id equals person.Id
                into persons

                from coachPerson in persons.DefaultIfEmpty()

                select coach;

            return Ok(coacheById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает менеджера игрока по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Результат выполнения запроса с менеджером игрока.</returns>
        [HttpGet("GetPlayerManagersById")]
        public IActionResult GetPlayerManagerById(Guid id)
        {
            ValidateId(id);

            var playerManagerById =
                from playerManager in _footballClubDbContext.PlayerManagers.ToList()
                where playerManager.Id == id

                join person in _footballClubDbContext.Persons
                on playerManager.Id equals person.Id
                into persons

                from playerManagerPerson in persons.DefaultIfEmpty()

                select playerManager;

            return Ok(playerManagerById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает взыскание с сотрудника по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Результат выполнения запроса с взысканием с сотрудника.</returns>
        [HttpGet("GetEmployeeRecoveriesById")]
        public IActionResult GetEmployeeRecoveryById(Guid id)
        {
            ValidateId(id);

            var employeeRecoveriesById =
                from employeeRecovery in _footballClubDbContext.EmployeeRecoveries
                where employeeRecovery.Id == id

                select employeeRecovery;

            return Ok(employeeRecoveriesById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает матч по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Результат выполнения запроса матчем.</returns>
        [HttpGet("GetMatchesById")]
        public IActionResult GetMatchById(Guid id)
        {
            ValidateId(id);

            var matchesById =
                from match in _footballClubDbContext.Matches
                where match.Id == id

                select match;

            return Ok(matchesById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает дисквалификацию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Статус выполнения запроса с дисквалификацией</returns>
        [HttpGet("GetDisqualificationsById")]
        public IActionResult GetDisqualificationById(Guid id)
        {
            ValidateId(id);

            var disqualificationsById =
                from disqualification in _footballClubDbContext.Disqualifications
                where disqualification.Id == id

                select disqualification;

            return Ok(disqualificationsById.FirstOrDefault());
        }

        /// <summary>
        /// Возвращает объект options игроков для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options игроков.</returns>
        [HttpGet("GetPlayerOptions")]
        public IActionResult GetPlayersOptions(int from = 0, int count = 15)
        {
            ValidateIntervalParams(from, count);

            var playersOptions =
                from playerOption in _footballClubDbContext.Players.Skip(@from).Take(count).ToList()

                join person in _footballClubDbContext.Persons
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

        /// <summary>
        /// Возвращает объект options контрактов для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options контрактов.</returns>
        [HttpGet("GetContractOptions")]
        public IActionResult GetContactsOptions(int from = 0, int count = 15)
        {
            ValidateIntervalParams(from, count);

            var contractsOptions =
                from contractOption in _footballClubDbContext.Contracts.Skip(@from).Take(count)

                select new
                {
                    Id = contractOption.Id,
                    DisplayValue = $"{_localizer["ContractSum"].Value} {contractOption.Sum}"
                };
            
            return Ok(contractsOptions);
        }

        /// <summary>
        /// Возвращает объект options сотрудников для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options сотрудников.</returns>
        [HttpGet("GetPersonOptions")]
        public IActionResult GetPersonsOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var personsOptions =
                from personOption in _footballClubDbContext.Persons.Skip(@from).Take(count)

                select new
                {
                    Id = personOption.Id,
                    DisplayValue = personOption.Name
                };

            return Ok(personsOptions);
        }

        /// <summary>
        /// Возвращает объект options причин взысканий для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options причин взысканий.</returns>
        [HttpGet("GetRecoveryReasonOptions")]
        public IActionResult GetRecoveryReasonsOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var recoveryReasonOptions =
                from recoveryReasonOption in _footballClubDbContext.RecoveryReasons.Skip(@from).Take(count)

                select new
                {
                    Id = recoveryReasonOption.Id,
                    DisplayValue = recoveryReasonOption.DisplayName
                };

            return Ok(recoveryReasonOptions);
        }

        /// <summary>
        /// Возвращает объект options результатов матчей для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options результатов матчей.</returns>
        [HttpGet("GetMatchResultOptions")]
        public IActionResult GetMatchResultsOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var matchResultOptions =
                from matchResultOption in _footballClubDbContext.MatchResults.Skip(@from).Take(count)

                select new
                {
                    Id = matchResultOption.Id,
                    DisplayValue = matchResultOption.DisplayName
                };

            return Ok(matchResultOptions);
        }

        /// <summary>
        /// Возвращает объект options менеджеров игроков для отображения в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options менеджеров игроков.</returns>
        [HttpGet("GetPlayerManagerOptions")]
        public IActionResult GetPlayerManagersOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var playerManagersOptions =
                from playerManagersOption in _footballClubDbContext.PlayerManagers.Skip(@from).Take(count).ToList()

                join person in _footballClubDbContext.Persons
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

        /// <summary>
        /// Возвращает схему объекта на русском языке.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <returns>Результат выполнения запроса со схемой объекта.</returns>
        [HttpGet("GetRuEntitySchema")]
        public IActionResult GetRuEntitySchema(string entityName)
        {
            if (string.IsNullOrEmpty(entityName))
            {
                throw new ArgumentException("Entity name can not be null or empty");
            }

            var dbName = _configuration.GetValue<string>("DbName");
            var schemas =
                from schema in _informationSchemaContext.EntitySchemas
                where (schema.TableSchema == dbName) && (schema.TableName == entityName)
                orderby schema.OrdinalPosition
                select new
                {
                    TableName = schema.TableName,
                    LocalizedColumnName = _localizer[schema.ColumnName].Value,
                    DataBaseColumnName = schema.ColumnName
                };

            return Ok(schemas.ToList());
        }

        /// <summary>
        /// Возвращает игроков на указанном интервале. В основном используется для получения игроков для секции.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Игроков со статусом запроса</returns>
        [HttpGet("GetPlayers")]
        public IActionResult GetPlayers(int from = 0, int count = 9)
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
        [HttpGet("GetCoaches")]
        public IActionResult GetCoaches(int from = 0, int count = 9)
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
        [HttpGet("GetMatches")]
        public IActionResult GetMatches(int from = 0, int count = 9)
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
        [HttpGet("GetDisqualifications")]
        public IActionResult GetDisqualifications(int from = 0, int count = 9)
        {
            var disqualifications =
                from disqualification in _footballClubDbContext.Disqualifications.Skip(@from).Take(count).ToList()

                join person in _footballClubDbContext.Persons
                on disqualification.PersonId equals person.Id
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
        [HttpGet("GetPlayerManagers")]
        public IActionResult GetPlayerManagers(int from = 0, int count = 9)
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
        [HttpGet("GetEmployeeRecoveries")]
        public IActionResult GetEmployeeRecoveries(int from = 0, int count = 9)
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
            ValidateEntity(person);

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
            ValidateEntity(player);

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
            ValidateEntity(coach);

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
            ValidateEntity(contract);

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
            ValidateEntity(playerManager);

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
            ValidateEntity(employeeRecovery);

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
            ValidateEntity(match);

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
            ValidateEntity(disqualification);

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
        public IActionResult DeleteEntity(string entityName, Guid id)
        {
            ValidateEntityNameAndId(entityName, id);

            _footballClubDbContext.Database.ExecuteSqlRaw("DELETE FROM footballclub." + entityName + " WHERE Id = {0}", id);

            return Ok();
        }

        /// <summary>
        /// Валидирует название сущности и её идентификатор.
        /// </summary>
        /// <param name="entityName">Название сущности/param>
        /// <param name="id">Идентификатор сущности</param>
        private void ValidateEntityNameAndId(string entityName, Guid id)
        {
            ValidateEntityName(entityName);
            ValidateId(id);
        }

        /// <summary>
        /// Валидирует идентификатор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <exception cref="ArgumentException">Исключение пробрасывается, когда идентификатор пустой.</exception>
        private void ValidateId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} can not be empty.");
            }
        }

        /// <summary>
        /// Валидирует имя сущности.
        /// </summary>
        /// <param name="entityName">Имя сущности</param>
        private void ValidateEntityName(string entityName)
        {
            if (string.IsNullOrEmpty(entityName))
            {
                throw new ArgumentException($"{nameof(entityName)} can not be null or empty.");
            }
        }

        /// <summary>
        /// Валидирует интервальные параметры.
        /// </summary>
        /// <param name="from">От</param>
        /// <param name="count">Количество</param>
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

        /// <summary>
        /// Валидирует сущность на null.
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <exception cref="ArgumentNullException">Генерируется, если сущность null.</exception>
        private void ValidateEntity(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can not be null");
            }
        }
    }
}
