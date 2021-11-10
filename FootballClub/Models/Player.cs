using System;

namespace FootballClub.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public Guid? ContractId { get; set; }

        public Contract Contract { get; set; }

        public Guid? PlayerManagerId { get; set; }

        public PlayerManager PlayerManager { get; set; }
    }
}
