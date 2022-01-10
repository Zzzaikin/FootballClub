using System;

namespace FootballClub.Models
{
    public class Match : BaseEntity
    {
        public DateTime Date { get; set; }

        public string OurTeamName { get; set; }

        public string EnemyTeamName { get; set; }

        public int Duration { get; set; }

        public int OurTeamScores { get; set; }

        public int EnemyTeamScores { get; set; }

        public bool IsViziting { get; set; }

        public bool WithFirstOvertime { get; set; }

        public bool WithSecondOvertime { get; set; }

        public bool PenaltyShootOut { get; set; }

        public Guid? TournamentId { get; set; }

        public Tournament Tournament { get; set; }
    }
}
