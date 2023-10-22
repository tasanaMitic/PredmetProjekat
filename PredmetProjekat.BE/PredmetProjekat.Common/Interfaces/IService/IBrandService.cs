using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IBrandService
    {
        BrandDtoId AddBrand(BrandDto brandDto);
        IEnumerable<BrandDtoId> GetBrands();
        IEnumerable<BrandDtoId> DeleteBrand(Guid id);
    }
}
