namespace SportSystem.Data
{
    using Bookmarks.Data.Repositories;
    using SportSystem.Models;

    public interface ISportSystemData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Match> Matches { get; }

        IRepository<Player> Players { get; }

        IRepository<Team> Teams { get; }

        IRepository<UserMatchBet> UserMatchBets { get; }

        IRepository<Vote> Votes { get; }

        int SaveChanges();
    }
}
