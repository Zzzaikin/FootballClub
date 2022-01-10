using System;

namespace FootballClub.Models
{
    public class Tournament : BaseEntity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
