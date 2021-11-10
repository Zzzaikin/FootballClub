using System;

namespace FootballClub.Models
{
    public class EmployeeRecovery
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Person Person { get; set; }

        public Guid? RecoveryReasonId { get; set; }

        public RecoveryReason RecoveryReason { get; set; }

        public string Result { get; set; }
    }
}
