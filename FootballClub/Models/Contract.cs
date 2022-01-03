using System;

namespace FootballClub.Models
{
    public class Contract : BaseEntity
    {
        public float Sum { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
