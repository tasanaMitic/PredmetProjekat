using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IBrandService
    {
        Guid AddBrand(BrandDto brandDto);
        IEnumerable<BrandDtoId> GetBrands();
        void DeleteBrand(Guid id);
    }
}
