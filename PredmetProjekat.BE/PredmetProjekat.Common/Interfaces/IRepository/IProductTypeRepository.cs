using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IProductTypeRepository : IGenericRepository<ProductType>
    {
        IEnumerable<ProductType> GetAllProductTypes();
        void CreateProductType(ProductType productType);
        void DeleteProductType(ProductType productType);
        ProductType GetProductTypeById(Guid id);
    }
}
