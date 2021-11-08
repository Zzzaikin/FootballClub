using System;

namespace FootballClub.Models
{
    public class EmployeeRecovery
    {
        public Guid Id { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? RecoveryReasonId { get; set; }

        public string Result { get; set; }
    }
}
