using System.Linq;

namespace SoftUni.Blog.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        T Add(T entity);

        T Update(T entity);

        T Remove(object id);

        void Remove(T entity);

        void SaveChanges();
    }
}
