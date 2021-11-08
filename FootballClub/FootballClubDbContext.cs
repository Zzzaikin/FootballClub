using FootballClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballClub
{
    public class FootballClubDbContext : DbContext
    {
        public FootballClubDbContext(DbContextOptions<FootballClubDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }
    }
}
