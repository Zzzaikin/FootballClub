using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class PlayerManager
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        [NotMapped]
        public bool WhetherToLoadPerson { get => true; }

        public Guid? PlayerId { get; set; }

        public List<Player> Player { get; set; }

        public float HoursPayment { get; set; }
    }
}
