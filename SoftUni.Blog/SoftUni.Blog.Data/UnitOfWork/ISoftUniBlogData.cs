namespace SoftUni.Blog.Data.UnitOfWork
{
    using Repositories;
    using Models;

    public interface ISoftUniBlogData
    {
        IRepository<User> Users { get; }

        IRepository<Post> Posts { get; }

        void SaveChanges();
    }
}
