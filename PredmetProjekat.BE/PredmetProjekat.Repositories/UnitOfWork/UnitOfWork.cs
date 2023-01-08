﻿using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.Repositories;

namespace PredmetProjekat.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreContext _context;
        public IBrandRepository BrandRepository { get; private set; } 
        public ICategoryRepository CategoryRepository { get; private set; }
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            BrandRepository = new BrandRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}