using Microsoft.EntityFrameworkCore;
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
            product.IsDeleted = true;
            Update(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(x => x.Category).Where(x => x.IsDeleted == false).Include(x => x.Brand).ToList();  
        }

        public IEnumerable<Product> GetAllStockedProducts()
        {
            return _context.Products.Include(x => x.Category).Include(x => x.Brand).Where(x => x.IsInStock == true && x.Quantity > 0 && x.IsDeleted == false).ToList();
        }

        public Product GetProductById(string productId)
        {
            return GetById(productId);
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
    }
}
