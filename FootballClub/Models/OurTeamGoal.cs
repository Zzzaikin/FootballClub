using System;

namespace FootballClub.Models
{
    public class OurTeamGoal : Goal
    {
        public Guid? AuthorPlayerId { get; set; }

        public Guid? TouchdownPassPlayerId { get; set; }

        public string OwnGoalPlayerName { get; set; }
    }
}
