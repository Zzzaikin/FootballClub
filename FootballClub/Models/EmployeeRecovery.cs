using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class EmployeeRecovery
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        [NotMapped]
        public bool WhetherToLoadPerson { get => false; }

        public Guid? RecoveryReasonId { get; set; }

        public RecoveryReason RecoveryReason { get; set; }

        public float Sum { get; set; }

        public DateTime Date { get; set; }

    }
}
