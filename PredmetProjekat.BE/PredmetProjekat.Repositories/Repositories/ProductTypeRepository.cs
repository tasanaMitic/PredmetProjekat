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
            return GetAll();
        }

        public ProductType GetProductTypeById(Guid id)
        {
            return GetById(id);
        }
    }
}
