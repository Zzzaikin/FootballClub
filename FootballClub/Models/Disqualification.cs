using System;

namespace FootballClub.Models
{
    public class Disqualification
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid? PlayerId { get; set; }

        public Player Player { get; set; }
    }
}
