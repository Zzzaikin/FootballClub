using System;

namespace FootballClub.Models
{
    public class Coach
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public float HoursPayment { get; set; }
    }
}
