using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
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
    public class EntitySchemaController : FootballClubBaseController<EntitySchemaController>
    {
        /// <summary>
        /// Инициализирует контексты базы данных, логгер, конфигурацию и локализатор.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="footballClubDbContext">Контекст базы данных футбольного клуба</param>
        /// <param name="localizer">Локализатор.</param>
        /// <param name="informationSchemaContext">Контекст базы данных схемы объектов</param>
        /// <param name="configuration">Конфигурация</param>
        public EntitySchemaController(IStringLocalizer<EntitySchemaController> localizer, ILogger<EntitySchemaController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

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

            var dbName = Configuration.GetValue<string>("DbName");
            var schemas =
                from schema in InformationSchemaContext.EntitySchemas
                where (schema.TableSchema == dbName) && (schema.TableName == entityName)
                orderby schema.OrdinalPosition
                select new
                {
                    TableName = schema.TableName,
                    LocalizedColumnName = Localizer[schema.ColumnName].Value,
                    DataBaseColumnName = schema.ColumnName,
                    DataType = schema.DataType
                };

            return Ok(schemas);
        }
    }
}