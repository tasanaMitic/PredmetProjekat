using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        IEnumerable<Brand> GetAllBrands();
        Brand GetBrandById(Guid brandId);
        void CreateBrand(Brand brand);
        void DeleteBrand(Brand brand);
    }
}
