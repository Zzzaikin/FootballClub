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
        /// Инициализирует контекст базы данных схем объектов, контекст базы данных внешних ключей,
        /// логгер, конфигурацию и локализатор.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="localizer">Локализатор.</param>
        /// <param name="informationSchemaContext">Контекст базы данных схем объектов</param>
        /// <param name="keyColumnUsageContext">Контекст базы данных внешних ключей</param>
        /// <param name="configuration">Конфигурация</param>
        public EntitySchemaController(IStringLocalizer<EntitySchemaController> localizer, ILogger<EntitySchemaController> logger,
            InformationSchemaContext informationSchemaContext, KeyColumnUsageContext keyColumnUsageContext, IConfiguration configuration)
            : base(localizer, logger, informationSchemaContext, keyColumnUsageContext, configuration)
        { }

        /// <summary>
        /// Возвращает схему объекта на русском языке.
        /// </summary>s
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
                    DataType = schema.DataType,
                    ColumnName = schema.ColumnName
                };

            return Ok(schemas);
        }

        /// <summary>
        /// Возвращает названия таблиц, на которые ссылается колонка.
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="columnName">Название колонки</param>
        /// <returns>Статус выполнения запроса с названием таблиц, на которые ссылается колонка.</returns>
        /// <exception cref="ArgumentException">Генерируется, когда название колонки или название таблицы пустые или null.</exception>
        [HttpGet("GetReferencedTableName")]
        public IActionResult GetReferencedTableName(string tableName, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentException("Column name can not be null or empty");
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Table name can not be null or emty");
            }

            var referencedTableNames =
                from foreignKeysSchema in KeyColumnUsageContext.ForeignKeysSchemas
                where
                    (foreignKeysSchema.ConstraintSchema == "footballclub") &&
                    (foreignKeysSchema.ColumnName == columnName) &&
                    (foreignKeysSchema.TableName == tableName)

                select foreignKeysSchema.ReferencedTableName;

            return Ok(referencedTableNames.FirstOrDefault());
        }
    }
}