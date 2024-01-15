using PredmetProjekat.Common.Interfaces.IRepository;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository BrandRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IRegisterRepository RegisterRepository { get; }
        IReceiptRepository ReceiptRepository { get; } 
        ISoldProductRepository SoldProductRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        void SaveChanges();
    }
}
