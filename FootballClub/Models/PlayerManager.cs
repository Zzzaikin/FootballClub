using System;
using System.Collections.Generic;

namespace FootballClub.Models
{
    public class PlayerManager : BaseEntity
    {
        public PlayerManager()
        {
            WhetherToLoadPerson = true;
        }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public Guid? PlayerId { get; set; }

        public List<Player> Player { get; set; }

        public float HoursPayment { get; set; }
    }
}
