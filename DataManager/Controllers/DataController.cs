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
        private readonly IConfiguration _configuration;

        private readonly ConnectionManager _connectionManager;

        private readonly MySqlConnection _connection;

        private readonly string _footballClubConnectionString;

        public DataController(IConfiguration configuration)
        {
            _connectionManager = ConnectionManager.GetInstance(_footballClubConnectionString);
            _connectionManager.OpenConnection();

            _configuration = configuration;
            _footballClubConnectionString = _configuration.GetConnectionString("FootballClub");
            _connection = _connectionManager.Connection; ;
        }

        ~DataController()
        {
            _connectionManager.CloseConnection();
        }

        [HttpPost("DeleteEntity")]
        public IActionResult DeleteEntity([FromBody] QueryModel baseQueryModel)
        {
            var deleteQuery = new DeleteQuery(_connection, baseQueryModel);
            var result = deleteQuery.PushAsync().Result;

            return Ok(result.AffectedRows);
        }

        [HttpPost("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords([FromBody] QueryModel baseQueryModel)
        {
            var countQuery = new CountQuery(_connection, baseQueryModel);
            var result = countQuery.PushAsync().Result;

            return Ok(result.RecordsCount);
        }

        [HttpPost("GetEntities")]
        public IActionResult GetEntities([FromBody] QueryModel selectQueryModel)
        {
            var selectQuery = new SelectQuery(_connection, selectQueryModel);
            var data = selectQuery.PushAsync().Result;

            return Ok(data.Records);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity([FromBody] QueryModel insertQueryModel)
        {
            var insertQuery = new InsertQuery(_connection, insertQueryModel);
            var result = insertQuery.PushAsync().Result;

            return Ok(result.AffectedRows);
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity([FromBody] QueryModel updateQueryModel)
        {
            var updateQuery = new UpdateQuery(_connection, updateQueryModel);
            var result = updateQuery.PushAsync().Result;

            return Ok(result);
        }
    }
}
