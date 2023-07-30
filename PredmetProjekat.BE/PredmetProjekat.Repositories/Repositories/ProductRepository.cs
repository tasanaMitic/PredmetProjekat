using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Dtos;
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

        public bool DeleteProductsByBrand(Guid brandId)     //not used atm
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

        public bool DeleteProductsByCategory(Guid categoryId)   //not used atm
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

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Products.Include(x => x.Category).Include(x => x.Brand).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Product> GetAllStockedProducts()
        {
            try
            {
                return _context.Products.Include(x => x.Category).Include(x => x.Brand).Where(x => x.IsInStock == true && x.Quantity > 0).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
