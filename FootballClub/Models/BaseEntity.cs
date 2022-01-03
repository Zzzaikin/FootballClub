using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        [NotMapped]
        public bool WhetherToLoadPerson { get; set; }
    }
}
