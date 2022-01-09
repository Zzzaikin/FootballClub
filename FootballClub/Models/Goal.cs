using System;

namespace FootballClub.Models
{
    public class Goal : BaseEntity
    {
        public int Time { get; set; }

        public Guid? MatchId { get; set; }
    }
}
