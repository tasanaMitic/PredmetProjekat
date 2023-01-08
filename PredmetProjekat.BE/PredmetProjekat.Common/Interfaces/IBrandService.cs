using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IBrandService
    {
        Guid AddBrand(BrandDto brandDto);
        IEnumerable<BrandDtoId> GetBrands();
        bool DeleteBrand(Guid id);
    }
}
