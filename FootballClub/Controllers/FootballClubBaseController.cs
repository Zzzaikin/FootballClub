using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;

namespace FootballClub.Controllers
{
    public class FootballClubBaseController<TController> : ControllerBase
    {
        /// <summary>
        /// Локализатор.
        /// </summary>
        protected IStringLocalizer<TController> Localizer { get; private set; }

        /// <summary>
        /// Логгер.
        /// </summary>
        protected ILogger<TController> Logger { get; private set; }

        /// <summary>
        /// Контекст базы данных футбольного клуба.
        /// </summary>
        protected FootballClubDbContext FootballClubDbContext { get; private set; }

        /// <summary>
        /// Контекст базы данных схемы объектов.
        /// </summary>
        protected InformationSchemaContext InformationSchemaContext { get; private set; }

        /// <summary>
        /// Конфигурация.
        /// </summary>
        protected IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Инициализирует контексты базы данных футбольного клуба и схемы таблиц, логгер, конфигурацию и локализатор.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="footballClubDbContext">Контекст базы данных футбольного клуба</param>
        /// <param name="localizer">Локализатор.</param>
        /// <param name="informationSchemaContext">Контекст базы данных схемы объектов</param>
        /// <param name="configuration">Конфигурация</param>
        public FootballClubBaseController(IStringLocalizer<TController> localizer, ILogger<TController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
        {
            Localizer = localizer;
            FootballClubDbContext = footballClubDbContext;
            Logger = logger;
            InformationSchemaContext = informationSchemaContext;
            Configuration = configuration;
        }

        /// <summary>
        /// Валидирует название сущности и её идентификатор.
        /// </summary>
        /// <param name="entityName">Название сущности/param>
        /// <param name="id">Идентификатор сущности</param>
        protected void ValidateEntityNameAndId(string entityName, Guid id)
        {
            ValidateEntityName(entityName);
            ValidateId(id);
        }

        /// <summary>
        /// Валидирует идентификатор.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <exception cref="ArgumentException">Исключение пробрасывается, когда идентификатор пустой.</exception>
        protected void ValidateId(Guid id)
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
        protected void ValidateEntityName(string entityName)
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
        protected void ValidateIntervalParams(int from, int count)
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
        protected void ValidateEntity(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity can not be null");
            }
        }
    }
}
