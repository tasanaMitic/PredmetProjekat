using System.Linq.Expressions;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        T GetByUsername(string username);
        IEnumerable<T> GetAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
