using FootballClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballClub
{
    public class KeyColumnUsageContext : DbContext
    {
        public KeyColumnUsageContext(DbContextOptions<KeyColumnUsageContext> options) : base(options) { }

        public DbSet<ForeignKeysSchema> ForeignKeysSchemas { get; set; }
    }
}
