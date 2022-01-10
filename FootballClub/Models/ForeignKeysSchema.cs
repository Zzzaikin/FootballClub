using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    [Keyless]
    [Table("KEY_COLUMN_USAGE")]
    public class ForeignKeysSchema
    {
        [Column("COLUMN_NAME")]
        public string ColumnName { get; set; }

        [Column("REFERENCED_TABLE_NAME")]
        public string ReferencedTableName { get; set; }

        [Column("CONSTRAINT_SCHEMA")]
        public string ConstraintSchema { get; set; }

        [Column("TABLE_NAME")]
        public string TableName { get; set; }
    }
}
