using Microsoft.AspNetCore.Mvc;
using System;

namespace FootballClub.Controllers
{
    /// <summary>
    /// Описывает базовые операции взаимодействия с базой данных.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityController<TEntity>
    {
        /// <summary>
        /// Пустой объект сущности.
        /// </summary>
        TEntity GetEmptyEntity();

        /// <summary>
        /// Возвращает количество записей игроков.
        /// </summary>
        /// <returns>Статус выполнения запроса с количеством записей игроков.</returns>
        IActionResult GetCountOfEntityRecords();

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        /// <returns>Статус выполнения запроса.</returns>
        IActionResult InsertEntity(TEntity entity);

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        /// <returns>Результат выполнения запроса.</returns>
        IActionResult UpdateEntity(TEntity entity);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        /// <returns>Результат выполнения запроса.</returns>
        IActionResult DeleteEntity(TEntity entity);

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Статус выполнения запроса с сущностью, найденной по идентификатору.</returns>
        IActionResult GetEntityById(Guid id);

        /// <summary>
        /// Возвращает объект options для отображения сущности в select.
        /// </summary>
        /// <param name="from">Параметр "От"</param>
        /// <param name="count">Параметр "Количество"</param>
        /// <returns>Результат выполнения запроса с объектом options.</returns>
        IActionResult GetEntityOptions(int from = 0, int count = 100);

        /// <summary>
        /// Возвращает сущности на указанном интервале.
        /// </summary>
        /// <param name="from">Параметр выборки "От"</param>
        /// <param name="count">Параметр определяющий количество получемых записей</param>
        /// <returns>Сущности со статусом запроса.</returns>
        IActionResult GetEntities(int from = 0, int count = 100);
    }
}
