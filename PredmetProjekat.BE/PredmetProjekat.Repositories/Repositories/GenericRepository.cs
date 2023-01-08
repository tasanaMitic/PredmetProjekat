using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Repositories.Context;
using System.Data;

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

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
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
