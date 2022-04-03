using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DataManager.Controllers
{
    /// <summary>
    /// Описывает базовые операции взаимодействия с базой данных.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataController
    {
        /// <summary>
        /// Возвращает название связанной таблицы по колонке.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <param name="columnName">Название колонки.</param>
        /// <returns>Название связанной таблицы по колонке</returns>
        IActionResult GetReferencedTableName(string entityName, string columnName);

        /// <summary>
        /// Возвращает схему таблицы сущности.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <returns>Статус выполнения запроса с схемой сущности.</returns>
        IActionResult GetEntitySchema(string entityName);

        /// <summary>
        /// Возвращает количество записей.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <returns>Статус выполнения запроса с количеством записей игроков.</returns>
        IActionResult GetCountOfEntityRecords(string entityName);

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        /// <param name="entityName">Название добавляемой сущности.</param>
        /// <param name="columnValues">Пара "Название колонки" - "Значение колонки".</param>
        /// <returns>Статус выполнения запроса с идентификатором добавленной сущности.</returns>
        IActionResult InsertEntity(string entityName, Dictionary<string, object> columnValues);

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entityName">Название обновляемой сущности.</param>
        /// <param name="columnValues">Пара "Название колонки" - "Значение колонки".</param>
        /// <returns>Результат выполнения запроса с идентификатором обновлённой сущности.</returns>
        IActionResult UpdateEntity(string entityName, Dictionary<string, object> columnValues);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entityName">Название удаляемой сущности.</param>
        /// <param name="entityId">Идентификатор удаляемой сущности.</param>
        /// <returns>Результат выполнения запроса.</returns>
        IActionResult DeleteEntity(string entityName, Guid entityId);

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Статус выполнения запроса с сущностью, найденной по идентификатору.</returns>
        IActionResult GetEntityById(string entityName, Guid id);

        /// <summary>
        /// Возвращает объект options для отображения сущности в select.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <param name="columnName">Название колонки, значение которой будет возвращено для отображения в select.</param>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options.</returns>
        IActionResult GetEntitiesOptions(string entityName, string columnName, int from = 0, int count = 100);

        /// <summary>
        /// Возвращает сущности на указанном интервале.
        /// </summary>
        /// <param name="entityName">Название сущности.</param>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Сущности со статусом запроса.</returns>
        IActionResult GetEntities(string entityName, int from = 0, int count = 100);
    }
}
