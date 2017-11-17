using Microsoft.AspNet.Identity.EntityFramework;
using SportSystem.Models;
namespace SportSystem.Data
{
    using System.Data.Entity;

    using SportSystem.Data.Migrations;

    public class SportSystemDbContext : IdentityDbContext<ApplicationUser>, ISportSystemDbContext
    {
        public SportSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportSystemDbContext, Configuration>());
        }

        public static SportSystemDbContext Create()
        {
            return new SportSystemDbContext();
        }

        public IDbSet<Match> Matches { get; set; }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<Player> Players { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<UserMatchBet> UserMatchBets { get; set; }

        public IDbSet<Vote> Votes { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().HasRequired(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>().HasRequired(x => x.HomeTeam)
                .WithMany(x => x.HomeMatches)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
