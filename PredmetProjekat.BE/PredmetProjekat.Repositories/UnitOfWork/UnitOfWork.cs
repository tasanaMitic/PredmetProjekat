using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.Repositories;

namespace PredmetProjekat.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoreContext _context;
        public IBrandRepository BrandRepository { get; private set; } 
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IRegisterRepository RegisterRepository { get; private set; }
        public IAdminRepository AdminRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public UnitOfWork(StoreContext context)
        {
            _context = context;

            BrandRepository = new BrandRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
            RegisterRepository = new RegisterRepository(_context);
            AdminRepository = new AdminRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
