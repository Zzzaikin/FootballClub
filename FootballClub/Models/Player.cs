using System;
using System.Collections.Generic;

namespace FootballClub.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public Guid? ContractId { get; set; }

        public List<Contract> Contract { get; set; }

        public Guid? PlayerManagerId { get; set; }

        public List<PlayerManager> PlayerManager { get; set; }
    }
}
