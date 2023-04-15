using System.Linq.Expressions;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        T GetByUsername(string username);
        IEnumerable<T> GetAll();
        void Add(T entity);
        bool Remove(Guid id);
        bool RemoveByUsername(string username);
        void Update(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
