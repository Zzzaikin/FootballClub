using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Common.Argument;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using DataManager.Extensions;
using System.Text;
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

        private MySqlConnection _footballClubConnection;

        private ConnectionManager _connectionManager;

        private string _footballClubConnectionString { get => _configuration.GetConnectionString("FootballClub"); }

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionManager = ConnectionManager.GetInstance(_footballClubConnectionString);
            _connectionManager.OpenConnection();
        }

        ~DataController()
        {
            _connectionManager.CloseConnection();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(string entityName, Guid entityId)
        {
            Argument.ValidateStringByAllPolicies(entityName, nameof(entityName));
            Argument.GuidNotEmpty(entityId, nameof(entityId));

            var sqlExpression =
                $"DELETE FROM {entityName} " +
                "WHERE Id = @id";

            var sqlCommand = new MySqlCommand(sqlExpression, _footballClubConnection);

            sqlCommand.Parameters.AddWithValue("@id", entityId);

            var queryExecution = ExecuteNonQueryAsync(sqlCommand);
            return queryExecution.Result;
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords(string entityName)
        {
            Argument.ValidateStringByAllPolicies(entityName, nameof(entityName));

            var sqlExpression = $"SELECT COUNT(*) FROM {entityName}";
            var sqlCommand = new MySqlCommand(sqlExpression, _footballClubConnection);
            var queryExecution = GetCountAsync(sqlCommand);

            return queryExecution.Result;
        }

        [HttpPost("GetEntities")]
        public IActionResult GetEntities([FromBody] SelectQueryModel selectQueryModel)
        {
            var selectQuery = new SelectQuery(_footballClubConnectionString, _connectionManager.Connection, selectQueryModel);
            var data = selectQuery.PushAsync();

            return Ok(data.Result.Records);
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

        private async Task<IActionResult> GetColumnNamesWithValuesAsync(MySqlCommand sqlCommand)
        {
            try
            {
                var dataTable = new DataTable();
                var dataAdapter = new MySqlDataAdapter(sqlCommand);

                await dataAdapter.FillAsync(dataTable);

                var columns = dataTable.Columns;
                var rows = dataTable.Rows;

                var columnNamesWithValues = new List<Dictionary<string, object>>();

                for (var i = 0; i < rows.Count; i++)
                {
                    var columnNameWithValue = new Dictionary<string, object>();

                    for (var j = 0; j < columns.Count; j++)
                    {
                        var columnName = columns[j].ColumnName;
                        var columnValue = rows[i][columnName];

                        columnNameWithValue.Add(columnName, columnValue);
                    }

                    columnNamesWithValues.Add(columnNameWithValue);
                }

                return Ok(columnNamesWithValues);
            }

            catch (Exception ex)
            {
                return GetProblemActionResult("Непредвиденная ошибка во время выполнения запроса к базе данных.", ex);
            }
        }

        private async Task<IActionResult> GetCountAsync(MySqlCommand sqlCommand)
        {
            try
            {
                using var reader = await sqlCommand.ExecuteReaderAsync();
                reader.Read();
                var count = reader.GetValue(0);

                return Ok(count);
            }

            catch (Exception ex)
            {
                return GetProblemActionResult("Непредвиденная ошибка во время выполнения запроса к базе данных.", ex);
            }
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

        private void OpenConnection()
        {
            var footballClubConnectionString = _configuration.GetConnectionString("FootballClub");
            _footballClubConnection = new MySqlConnection(footballClubConnectionString);

            if (_footballClubConnection.State == ConnectionState.Closed)
            {
                _footballClubConnection.Open();
            }
        }

    }
}
