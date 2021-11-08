using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class Coach
    {
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Person Person { get; set; }

        public float HoursPayment { get; set; }
    }
}
