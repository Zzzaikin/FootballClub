using System;

namespace FootballClub.Models
{
    public class OurTeamGoal : Goal
    {
        public Guid? Author { get; set; }

        public Guid? TouchdownPassFrom { get; set; }

        public string OwnGoalPlayerName { get; set; }
    }
}
