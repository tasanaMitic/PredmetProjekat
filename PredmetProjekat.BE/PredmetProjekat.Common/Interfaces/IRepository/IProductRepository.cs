using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllStockedProducts();
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
        Product GetProductById(Guid productId);
    }
}
