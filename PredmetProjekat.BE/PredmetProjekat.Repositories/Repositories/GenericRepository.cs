using Microsoft.EntityFrameworkCore;
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
                throw new KeyNotFoundException($"Entity with id: {id}, was not found in the database!");
            }
            return entity;            
        }

        public T GetByUsername(string username)
        {
            var entity = _context.Set<T>().Find(username);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            return entity;
        }

        //public bool RemoveByUsername(string username)
        //{
        //    try
        //    {
        //        var entity = _context.Set<T>().Find(username);
        //        _context.Set<T>().Remove(entity);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().RemoveRange(entities);
        //}

        //public void Update(T entity)    //todo
        //{
        //    try
        //    {
        //        _context.Set<T>().Update(entity);
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        throw new KeyNotFoundException();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        throw new DuplicateNameException();
        //    }
        //}

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
