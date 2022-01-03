using System;

namespace FootballClub.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }

        public string HomePhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
