using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IBrandService
    {
        Guid AddBrand(BrandDto brandDto);
        IEnumerable<BrandDto> GetBrands();
        bool DeleteBrand(Guid id);
    }
}
