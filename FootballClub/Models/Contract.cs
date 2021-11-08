using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class Contract
    {
        public Guid Id { get; set; }

        [Column("PlayerId")]
        public Player Player { get; set; }

        public float Sum { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
