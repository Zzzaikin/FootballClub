using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("Contract")]
    public class ContractsController : FootballClubBaseController<ContractsController>, IEntityController<Contract>
    {
        public ContractsController(IStringLocalizer<ContractsController> localizer, ILogger<ContractsController> logger, 
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetEmptyEntity")]
        public Contract GetEmptyEntity()
        {
            return new Contract();
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(Contract contract)
        {
            ValidateEntity(contract);

            FootballClubDbContext.Contracts.Remove(contract);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.Contracts.Count();
            return Ok(count);
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var contracts =
                 from contract in FootballClubDbContext.Contracts.Skip(@from).Take(count).ToList()
                 select contract;

            return Ok(contracts);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            ValidateId(id);

            var contractById =
                from contract in FootballClubDbContext.Coaches.ToList()
                where contract.Id == id

                select contract;

            return Ok(contractById.FirstOrDefault());
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            ValidateIntervalParams(from, count);

            var contractOptions =
                from contractOption in FootballClubDbContext.Contracts.Skip(@from).Take(count)

                select new
                {
                    Id = contractOption.Id,
                    DisplayValue = $"{contractOption.Sum} {Localizer["Currensy"].Value} " +
                    $"{contractOption.StartDate.ToShortDateString()}-{contractOption.EndDate.ToShortDateString()}"
                };

            return Ok(contractOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(Contract contract)
        {
            ValidateEntity(contract);

            FootballClubDbContext.Contracts.Add(contract);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(Contract contract)
        {
            ValidateEntity(contract);

            FootballClubDbContext.Contracts.Update(contract);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
