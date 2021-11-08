using System;

namespace FootballClub.Models
{
    public class PlayerManager
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? PlayerId { get; set; }

        public float HoursPayment { get; set; }
    }
}
