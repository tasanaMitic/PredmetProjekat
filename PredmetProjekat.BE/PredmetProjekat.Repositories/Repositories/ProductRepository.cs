using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public bool DeleteProductsByBrand(Guid brandId)
        {
            try
            {
                var productsToDelete = _context.Products.Where(p => p.Brand.BrandId == brandId);
                _context.Products.RemoveRange(productsToDelete);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProductsByCategory(Guid categoryId)
        {
            try
            {
                var productsToDelete = _context.Products.Where(p => p.Category.CategoryId == categoryId);
                _context.Products.RemoveRange(productsToDelete);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
