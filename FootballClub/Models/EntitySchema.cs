using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballClub.Models
{
    [Keyless]
    [Table("columns")]
    public class EntitySchema
    {
        [Column("COLUMN_NAME")]
        public string ColumnName { get; set; }

        [Column("TABLE_SCHEMA")]
        public string TableSchema { get; set; }

        [Column("TABLE_NAME")]
        public string TableName { get; set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; set; }
    }
}
