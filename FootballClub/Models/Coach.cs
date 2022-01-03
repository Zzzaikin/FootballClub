using System;

namespace FootballClub.Models
{
    public class Coach : BaseEntity
    {
        public Coach()
        {
            WhetherToLoadPerson = true;
        }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public float HoursPayment { get; set; }
    }
}
