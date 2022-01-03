using System;

namespace FootballClub.Models
{
    public class EmployeeRecovery : BaseEntity
    {
        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public Guid? RecoveryReasonId { get; set; }

        public RecoveryReason RecoveryReason { get; set; }

        public float Sum { get; set; }

        public DateTime Date { get; set; }

    }
}
