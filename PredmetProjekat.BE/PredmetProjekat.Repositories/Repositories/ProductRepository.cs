using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context) { }

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
            return _context.Products.Where(x => x.IsDeleted == false).Include(x => x.Category).Include(x => x.Brand).Include(x => x.AttributeValues).ThenInclude(x => x.ProductAttribute).Include(x => x.ProductType).ToList();  
        }

        public IEnumerable<Product> GetAllStockedProducts() 
        {
            return _context.Products.Where(x => x.IsInStock == true && x.Quantity > 0 && x.IsDeleted == false && x.Price > 0).Include(x => x.Category).Include(x => x.Brand).Include(x => x.AttributeValues).ThenInclude(x => x.ProductAttribute).Include(x => x.ProductType).ToList();
        }

        public Product GetProductById(string productId) 
        {
            return _context.Products.Where(x => x.IsDeleted == false && x.ProductId == productId).Include(x => x.Category).Include(x => x.Brand).Include(x => x.AttributeValues).ThenInclude(x => x.ProductAttribute).Include(x => x.ProductType).First();
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
    }
}
