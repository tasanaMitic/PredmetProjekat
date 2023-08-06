using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Repositories.Context;
using System.Data;
using System.Linq.Expressions;

namespace PredmetProjekat.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {      
            var entity = _context.Set<T>().Find(id);

            if (entity == null)
            {
                var type = typeof(T).ToString().Split('.').Last();
                throw new KeyNotFoundException($"{type} with id: {id}, was not found in the database!");
            }
            return entity;            
        }

        public T GetByUsername(string username)
        {
            var entity = _context.Set<T>().Find(username);
            if (entity == null)
            {
                var type = typeof(T).ToString().Split('.').Last();
                throw new KeyNotFoundException($"{type} with username: {username}, was not found in the database!");
            }
            return entity;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
