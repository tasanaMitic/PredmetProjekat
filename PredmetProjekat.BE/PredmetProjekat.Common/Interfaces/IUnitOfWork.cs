namespace PredmetProjekat.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IRegisterRepository RegisterRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IAdminRepository AdminRepository { get; }
    }
}
