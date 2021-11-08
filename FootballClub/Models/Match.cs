using System;

namespace FootballClub.Models
{
    public class Match
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid? MatchResultId { get; set; }

        public string Duration { get; set; }

        public int OurTeamScores { get; set; }

        public int EnemyTeamScores { get; set; }

        public bool WithFirstOvertime { get; set; }

        public bool WithSecondOvertime { get; set; }

        public bool PenaltyShootOut { get; set; }
    }
}
