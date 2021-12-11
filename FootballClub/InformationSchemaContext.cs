using FootballClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballClub
{
    public class InformationSchemaContext : DbContext
    {
        public InformationSchemaContext(DbContextOptions<InformationSchemaContext> options) : base(options) { }

        public DbSet<EntitySchema> EntitySchemas { get; set; }
    }
}
