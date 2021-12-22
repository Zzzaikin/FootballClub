using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class Disqualification
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        [NotMapped]
        public bool WhetherToLoadPerson { get => false; }
    }
}
