namespace SportSystem.Data
{
    using System.Data.Entity;
    
    using SportSystem.Models;

    public interface ISportSystemDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Match> Matches { get; set; }

        IDbSet<Team> Teams { get; set; }

        IDbSet<Player> Players { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<UserMatchBet> UserMatchBets { get; set; }

        IDbSet<Vote> Votes { get; set; } 

        int SaveChanges();
    }
}
