using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    public class EmployeeRecovery
    {
        public Guid Id { get; set; }

        [Column("PersonId")]
        public Person Person { get; set; }

        [Column("RecoveryReasonId")]
        public RecoveryReason RecoveryReason { get; set; }

        public string Result { get; set; }
    }
}
