using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.Repositories;
using System.Data;

namespace PredmetProjekat.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreContext _context;
        public IBrandRepository BrandRepository { get; private set; } 
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IRegisterRepository RegisterRepository { get; private set; }
        public IReceiptRepository ReceiptRepository { get; private set; }

        public UnitOfWork(StoreContext context)
        {
            _context = context;

            BrandRepository = new BrandRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            RegisterRepository = new RegisterRepository(_context);
            ReceiptRepository = new ReceiptRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex) //todo
            {
                throw new Exception();   //update?
            }
            catch (DbUpdateException ex)
            {
                throw new DuplicateNameException(); 
            }

        }
    }
}
