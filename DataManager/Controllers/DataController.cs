using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Common.Argument;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using QueryPush.Queries;
using QueryPush.Models.QueryModels;
using QueryPush;

namespace DataManager.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с БД.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase, IDataController
    {
        private IConfiguration _configuration;

        private MySqlConnection _footballClubConnection => _footballClubConnectionManager.Connection;

        private ConnectionManager _footballClubConnectionManager;

        private string _footballClubConnectionString { get => _configuration.GetConnectionString("FootballClub"); }

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;

            _footballClubConnectionManager = ConnectionManager.GetInstance(_footballClubConnectionString);
            _footballClubConnectionManager.OpenConnection();
        }

        ~DataController()
        {
            _footballClubConnectionManager.CloseConnection();
        }

        [HttpPost("DeleteEntity")]
        public IActionResult DeleteEntity([FromBody] BaseQueryModel baseQueryModel)
        {
            var deleteQuery = new DeleteQuery(_footballClubConnection, baseQueryModel);
            var result = deleteQuery.PushAsync().Result;

            return Ok(result);
        }

        [HttpPost("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords([FromBody] BaseQueryModel baseQueryModel)
        {
            var countQuery = new CountQuery(_footballClubConnection, baseQueryModel);
            var result = countQuery.PushAsync().Result;

            return Ok(result);
        }

        [HttpPost("GetEntities")]
        public IActionResult GetEntities([FromBody] SelectQueryModel selectQueryModel)
        {
            var selectQuery = new SelectQuery(_footballClubConnection, selectQueryModel);
            var data = selectQuery.PushAsync().Result;

            return Ok(data.Records);
        }

        public IActionResult GetEntitySchema(string entityName)
        {
            throw new NotImplementedException();
        }

        public IActionResult InsertEntity(string entityName, Dictionary<string, object> columnValues)
        {
            throw new NotImplementedException();
        }

        public IActionResult UpdateEntity(string entityName, Dictionary<string, object> columnValues)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetReferencedTableName(string entityName, string columnName)
        {
            throw new NotImplementedException();
        }

        private string GetColumnsString(List<string> columns)
        {
            if (columns.Count == 0)
            {
                columns.Add("*");
                return columns[0];
            }

            return string.Join(", ", columns);
        }

        private IActionResult GetProblemActionResult(string message, Exception ex)
        {
            Argument.StringNotNullOrEmpty(message, nameof(message));

            return Problem(
                    title: "Непредвиденная ошибка во время выполнения запроса к базе данных.",
                    detail: ex.ToString(),
                    statusCode: 500);
        }

        private async Task<IActionResult> ExecuteNonQueryAsync(MySqlCommand sqlCommand)
        {
            try
            {
                var count = await sqlCommand.ExecuteNonQueryAsync();
                return Ok(count);
            }

            catch (Exception ex)
            {
                return GetProblemActionResult("Непредвиденная ошибка во время выполнения запроса к базе данных.", ex);
            }
        }
    }
}
