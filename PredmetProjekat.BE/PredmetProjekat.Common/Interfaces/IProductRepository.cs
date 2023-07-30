using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        bool DeleteProductsByCategory(Guid categoryId);
        bool DeleteProductsByBrand(Guid brandId);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllStockedProducts();
    }
}
