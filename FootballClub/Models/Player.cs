using FootballClub.Controllers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Person Person { get; set; }

        [Column("ContractId")]
        public Contract Contract { get; set; }

        [Column("PlayerManagerId")]
        public PlayerManager PlayerManager { get; set; }

        [Column("DisqualificationId")]
        public Disqualification Disqualification { get; set; }
    }
}
