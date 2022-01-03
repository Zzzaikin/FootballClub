using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FootballClub.Controllers
{
    /// <summary>
    /// Контроллер для дашборда.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger<DashboardController> _logger;

        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly FootballClubDbContext _footballClubDbContext;

        /// <summary>
        /// Инициализирует контекст базы данных и логгер.
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="footballClubDbContext">Контекст базы данных</param>
        public DashboardController(ILogger<DashboardController> logger, FootballClubDbContext footballClubDbContext)
        {
            _logger = logger;
            _footballClubDbContext = footballClubDbContext;
        }

        /// <summary>
        /// Возвращает опции для графика recharts.
        /// </summary>
        /// <param name="startDate">Начальная дата</param>
        /// <param name="endDate">Конечная дата</param>
        /// <returns>Результат выполнения запроса с объектом options для графика recharts.</returns>
        [HttpGet("GetGraphycOptionsForEmployeeRecoveriesSection")]
        public IActionResult GetGraphycOptionsForEmployeeRecoveriesSection(DateTime startDate, DateTime endDate)
        {
            ValidateStartAndEndDates(startDate, endDate);

            var deltaYears = endDate.Year - startDate.Year;
            var deltaMonths = endDate.Month - startDate.Month;

            var graphycOptions = GetGraphycOptions(deltaYears, deltaMonths, startDate, endDate);

            return Ok(graphycOptions);
        }

        /// <summary>
        /// Возвращает опции для графика recharts с определением группировки по выбранному промежутку времени.
        /// </summary>
        /// <param name="deltaYears">Разность в годах между границами выбранного промежутка времени.</param>
        /// <param name="deltaMonths">Разность в месяцах между границами выбранного промежутка времени.param>
        /// <param name="startDate">Начальная дата.</param>
        /// <param name="endDate">Конечная дата.</param>
        /// <returns></returns>
        private IEnumerable GetGraphycOptions(int deltaYears, int deltaMonths, DateTime startDate, DateTime endDate)
        {
            ValidateStartAndEndDates(startDate, endDate);
            ValidateDateDeltaValues(deltaYears, deltaMonths);

            if (((deltaYears > 0) && (deltaYears < 5)) || (deltaMonths >= 6))
                return _footballClubDbContext.EmployeeRecoveries
                    .Where(e => (e.Date >= startDate) && (e.Date <= endDate))
                    .GroupBy(e => e.Date.Month)
                    .Select(e => new
                    {
                        Date = e.Key,
                        Sum = e.Sum(g => g.Sum)
                    }).ToList();

            else if (deltaYears >= 5)
                return _footballClubDbContext.EmployeeRecoveries
                    .Where(e => (e.Date >= startDate) && (e.Date <= endDate))
                    .GroupBy(e => e.Date.Year)
                    .Select(e => new
                    {
                        Sum = e.Sum(g => g.Sum)
                    }).ToList();

            return _footballClubDbContext.EmployeeRecoveries
                .Where(e => (e.Date >= startDate) && (e.Date <= endDate))
                .GroupBy(e => e.Date)
                .Select(e => new
                {
                    Sum = e.Sum(g => g.Sum)
                }).ToList();

        }

        /// <summary>
        /// Валидирует значения разности между начальной даты и конченой даты.
        /// </summary>
        /// <param name="deltaYears">Разность в годах между границами выбранного промежутка времени.</param>
        /// <param name="deltaMonths">Разность в месецах между границами выбранного промежутка времени.</param>
        private void ValidateDateDeltaValues(int deltaYears, int deltaMonths)
        {
            if (deltaYears < 0)
            {
                throw new ArgumentException($"{nameof(deltaYears)} can not be less than zero.");
            }

            if (deltaMonths < 0)
            {
                throw new ArgumentException($"{nameof(deltaMonths)} can not be less than zero.");
            }
        }

        /// <summary>
        /// Валидирует начальную дату и конечную дату.
        /// </summary>
        /// <param name="startDate">Начальная дата.</param>
        /// <param name="endDate">Конечная дата.</param>
        private void ValidateStartAndEndDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date can not be greater than end date.");
            }
        }
    }
}
