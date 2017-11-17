namespace StreamPowered.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface IStreamPoweredData
    {
        IRepository<User> Users { get; }

        IRepository<Game> Games { get; }

        IRepository<Genre> Genres { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<Review> Reviews { get; }

        void SaveChanges();
    }
}
