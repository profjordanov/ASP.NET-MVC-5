namespace Snippy.Data.Repositories
{
    using System.Linq;

    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        T Add(T entity);

        T Update(T entity);

        void Remove(T entity);

        T Remove(object id);

        void SaveChanges();
    }
}
