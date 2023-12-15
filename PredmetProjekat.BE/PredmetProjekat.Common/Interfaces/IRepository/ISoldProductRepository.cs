using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface ISoldProductRepository : IGenericRepository<SoldProduct>
    {
        IEnumerable<SoldProduct> GetSoldProductsByIds(IEnumerable<Guid> soldproductsIds);
        void CreateSoldProduct(SoldProduct soldProduct);
    }
}
