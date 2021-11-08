using System;

namespace FootballClub.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? ContractId { get; set; }

        public Guid? PlayerManagerId { get; set; }
    }
}
