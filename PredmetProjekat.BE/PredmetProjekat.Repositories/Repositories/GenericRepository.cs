using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces;
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
        public void Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DuplicateNameException();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public T GetById(Guid id)
        {      
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }
            return entity;            
        }

        public bool Remove(Guid id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RemoveByUsername(string username)
        {
            try
            {
                var entity = _context.Set<T>().Find(username);
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new KeyNotFoundException();
            }
            catch (DbUpdateException e)
            {
                throw new DuplicateNameException();
            }
        }
    }
}
