using System;

namespace FootballClub.Models
{
    public class EnemyTeamGoal : Goal
    {
        public string Author { get; set; }

        public string TouchdownPassFrom { get; set; }

        public Guid? OwnGoalPlayerId { get; set; }
    }
}
