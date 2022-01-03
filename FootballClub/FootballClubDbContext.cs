using FootballClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballClub
{
    public class FootballClubDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Coach> Coaches { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Disqualification> Disqualifications { get; set; }

        public DbSet<EmployeeRecovery> EmployeeRecoveries { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<MatchResult> MatchResults { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerManager> PlayerManagers { get; set; }

        public DbSet<RecoveryReason> RecoveryReasons { get; set; }
    }
}
