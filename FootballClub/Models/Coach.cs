using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class Coach
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        [NotMapped]
        public bool WhetherToLoadPerson { get => true; }

        public float HoursPayment { get; set; }
    }
}
