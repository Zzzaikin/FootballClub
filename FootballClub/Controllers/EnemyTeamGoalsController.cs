﻿using FootballClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FootballClub.Controllers
{
    public class EnemyTeamGoalsController : FootballClubBaseController<EnemyTeamGoalsController>, IEntityController<EnemyTeamGoal>
    {
        public EnemyTeamGoalsController(IStringLocalizer<EnemyTeamGoalsController> localizer, ILogger<EnemyTeamGoalsController> logger,
            FootballClubDbContext footballClubDbContext, InformationSchemaContext informationSchemaContext, IConfiguration configuration)
            : base(localizer, logger, footballClubDbContext, informationSchemaContext, configuration)
        { }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(EnemyTeamGoal enemyTeamGoal)
        {
            ValidateEntity(enemyTeamGoal);

            FootballClubDbContext.EnemyTeamGoals.Remove(enemyTeamGoal);
            var countOfDeletedRecords = FootballClubDbContext.SaveChanges();

            return countOfDeletedRecords > 0 ? Ok() : Problem(title: "No records has been deleted", statusCode: 500);
        }

        [HttpGet("GetCountOfEntityRecords")]
        public IActionResult GetCountOfEntityRecords()
        {
            var count = FootballClubDbContext.EnemyTeamGoals.Count();
            return Ok(count);
        }

        [HttpGet("GetEmptyEntity")]
        public EnemyTeamGoal GetEmptyEntity()
        {
            return new EnemyTeamGoal();
        }

        [HttpGet("GetEntities")]
        public IActionResult GetEntities(int from = 0, int count = 100)
        {
            ValidateIntervalParams(@from, count);

            var enemyTeamGoals =
                from person in FootballClubDbContext.EnemyTeamGoals.Skip(@from).Take(count)

                select person;

            return Ok(enemyTeamGoals);
        }

        [HttpGet("GetEntityById")]
        public IActionResult GetEntityById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetEntityOptions")]
        public IActionResult GetEntityOptions(int from = 0, int count = 100)
        {
            var enemyTeamGoalOptions =
                from enemyTeamGoalOption in FootballClubDbContext.EnemyTeamGoals.ToList()

                select new
                {
                    Id = enemyTeamGoalOption.Id,
                    DisplayName = $"{Localizer["Score_A_Goal"].Value} {enemyTeamGoalOption.Author}"
                };

            return Ok(enemyTeamGoalOptions);
        }

        [HttpPost("InsertEntity")]
        public IActionResult InsertEntity(EnemyTeamGoal enemyTeamGoal)
        {
            ValidateEntity(enemyTeamGoal);

            FootballClubDbContext.EnemyTeamGoals.Add(enemyTeamGoal);
            FootballClubDbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdateEntity")]
        public IActionResult UpdateEntity(EnemyTeamGoal enemyTeamGoal)
        {
            ValidateEntity(enemyTeamGoal);

            FootballClubDbContext.EnemyTeamGoals.Update(enemyTeamGoal);
            var countOfEditRecords = FootballClubDbContext.SaveChanges();

            return countOfEditRecords > 0 ? Ok() : Problem(title: "No records has been updated.", statusCode: 500);
        }
    }
}
