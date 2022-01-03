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
    public class RecoveryReasonsController : FootballClubBaseController<RecoveryReasonsController>
    {
        public RecoveryReasonsController(IStringLocalizer<RecoveryReasonsController> localizer, ILogger<RecoveryReasonsController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpGet("GetRecoveryReasonOptions")]
        public IActionResult GetRecoveryReasonsOptions(int from = 0, int count = 0)
        {
            ValidateIntervalParams(from, count);

            var recoveryReasonOptions =
                from recoveryReasonOption in FootballClubDbContext.RecoveryReasons.Skip(@from).Take(count)

                select new
                {
                    Id = recoveryReasonOption.Id,
                    DisplayValue = recoveryReasonOption.DisplayName
                };

            return Ok(recoveryReasonOptions);
        }
    }
}