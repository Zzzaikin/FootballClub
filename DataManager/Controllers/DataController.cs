using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Common.Argument;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using QueryPush.Queries;
using QueryPush;
using System.Text;
using QueryPush.Models;

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
        public IActionResult DeleteEntity([FromBody] QueryModel baseQueryModel)
        {
            var deleteQuery = new DeleteQuery(_footballClubConnection, baseQueryModel);
            var result = deleteQuery.PushAsync().Result;

            return Ok(result);
        }

        [HttpPost("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords([FromBody] QueryModel baseQueryModel)
        {
            var countQuery = new CountQuery(_footballClubConnection, baseQueryModel);
            var result = countQuery.PushAsync().Result;

            return Ok(result);
        }

        [HttpPost("GetEntities")]
        public IActionResult GetEntities([FromBody] QueryModel selectQueryModel)
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

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity([FromBody] QueryModel updateQueryModel)
        {
            var updateQuery = new UpdateQuery(_footballClubConnection, updateQueryModel);
            var result = updateQuery.PushAsync().Result;

            return Ok(result);
        }

        public IActionResult GetReferencedTableName(string entityName, string columnName)
        {
            throw new NotImplementedException();
        }
    }
}
