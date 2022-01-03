using System;

namespace FootballClub.Models
{
    public class Disqualification : BaseEntity
    {
        public string DisplayName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }
    }
}
