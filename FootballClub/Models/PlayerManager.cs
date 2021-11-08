using FootballClub.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Controllers
{
    public class PlayerManager
    {
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Person Person { get; set; }

        [Column("PlayerId")]
        public Player Player { get; set; }

        public float HoursPayment { get; set; }
    }
}
