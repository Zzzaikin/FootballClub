using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Common.Argument;
using Microsoft.Extensions.Localization;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using DataManager.Enums;
using System.Threading.Tasks;

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

        private MySqlConnection _footballClubConnection;

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;
            OpenConnection();
        }

        ~DataController()
        {
            if (_footballClubConnection.State == ConnectionState.Open)
            {
                _footballClubConnection.Close();
            }
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(string entityName, Guid entityId)
        {
            Argument.StringNotNullOrEmpty(entityName, nameof(entityName));
            Argument.GuidNotEmpty(entityId, nameof(entityId));
            Argument.StringNotContainsSqlInjections(entityName, nameof(entityName));

            var sqlExpression =
                $"DELETE FROM {entityName} " +
                "WHERE Id = @id";

            var sqlCommand = new MySqlCommand(sqlExpression, _footballClubConnection);

            sqlCommand.Parameters.AddWithValue("@id", entityId);

            var queryExecutionResult = PushQuery(sqlCommand, Execute.NonQueryAsync);
            return queryExecutionResult.Result;
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords(string entityName)
        {
            Argument.StringNotNullOrEmpty(entityName, nameof(entityName));
            Argument.StringNotContainsSqlInjections(entityName, nameof(entityName));

            var sqlExpression = $"SELECT COUNT(*) FROM {entityName}";
            var sqlCommand = new MySqlCommand(sqlExpression, _footballClubConnection);
            var queryExecutionResult = PushQuery(sqlCommand, Execute.ReaderAsync);

            return queryExecutionResult.Result;
        }

        public IActionResult GetEntities(string entityName, int from = 0, int count = 100)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetEntitiesOptions(string entityName, string columnName, int from = 0, int count = 100)
        {
            throw new NotImplementedException();
        }

        public IActionResult GetEntityById(string entityName, Guid id)
        {
            throw new NotImplementedException();
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

        private async Task<IActionResult> PushQuery(MySqlCommand sqlCommand, Execute execute)
        {
            switch (execute)
            {
                case Execute.NonQueryAsync:

                    try
                    {
                        var count = await sqlCommand.ExecuteNonQueryAsync();
                        return Ok(count);
                    }

                    catch (Exception ex)
                    {
                        return Problem(
                            title: "Непредвиденная ошибка во время выполнения запроса к базе данных.",
                            detail: ex.ToString(),
                            statusCode: 500);
                    }

                case Execute.ReaderAsync:

                    try
                    {
                        using var reader = await sqlCommand.ExecuteReaderAsync();
                        reader.Read();
                        var count = reader.GetValue(0);
                        return Ok(count);
                    }
                    
                    catch(Exception ex)
                    {
                        return Problem(
                            title: "Непредвиденная ошибка во время выполнения запроса к базе данных.",
                            detail: ex.ToString(),
                            statusCode: 500);
                    }

                default:
                    throw new NotImplementedException("Нет поведения для такого значения перечисления Execute.");
            }
        }

        private void OpenConnection()
        {
            var footballClubConnectionString = _configuration.GetConnectionString("FootballClub");
            _footballClubConnection = new MySqlConnection(footballClubConnectionString);

            if (_footballClubConnection.State == ConnectionState.Closed)
            {
                _footballClubConnection.Open();
            }
        }

        public IActionResult GetReferencedTableName(string entityName, string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
