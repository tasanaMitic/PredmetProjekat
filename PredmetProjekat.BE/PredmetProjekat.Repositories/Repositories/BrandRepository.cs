using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(StoreContext context) : base(context) { }

        public void CreateBrand(Brand brand)
        {
            Create(brand);
        }

        public void DeleteBrand(Brand brand)
        {
            brand.IsDeleted = true;
            Update(brand);
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return FindByCondition(x => x.IsDeleted == false);
        }

        public Brand GetBrandById(Guid brandId)
        {
            return GetById(brandId);
        }
    }
}
