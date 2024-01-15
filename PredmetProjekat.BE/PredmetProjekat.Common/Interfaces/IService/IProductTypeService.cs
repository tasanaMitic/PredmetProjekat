using PredmetProjekat.Common.Dtos.ProductDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IProductTypeService
    {
        ProductTypeDtoId AddProductType(ProductTypeDto productTypeDto);
        IEnumerable<ProductTypeDtoId> GetProductTypes();
        IEnumerable<ProductTypeDtoId> DeleteProductTypes(Guid id);
    }
}
