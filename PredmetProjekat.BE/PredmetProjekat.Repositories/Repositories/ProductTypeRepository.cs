using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(StoreContext context) : base(context) { }

        public void CreateProductType(ProductType productType)
        {
            Create(productType);
        }

        public void DeleteProductType(ProductType productType)
        {
            productType.IsDeleted = true;
            Update(productType);
        }

        public IEnumerable<ProductType> GetAllProductTypes()
        {
            return _context.ProductTypes.Where(x => x.IsDeleted == false)
                                        .Include(x => x.Attributes)
                                        .ToList();
        }

        public ProductType GetProductTypeById(Guid productTypeId)
        {
            return _context.ProductTypes.Where(x => x.IsDeleted == false && x.ProductTypeId == productTypeId)
                                        .Include(x => x.Attributes)
                                        .FirstOrDefault();
        }
    }
}
