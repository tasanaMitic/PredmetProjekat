using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(x => x.Category).Include(x => x.Brand).ToList();  
        }

        public IEnumerable<Product> GetAllStockedProducts()
        {
            try//todo
            {
                return _context.Products.Include(x => x.Category).Include(x => x.Brand).Where(x => x.IsInStock == true && x.Quantity > 0).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetProductById(Guid productId)
        {
            return GetById(productId);
        }
    }
}
