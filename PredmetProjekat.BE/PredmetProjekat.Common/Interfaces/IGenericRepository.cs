﻿using System.Linq.Expressions;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        bool Remove(Guid id);
        void Update(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
