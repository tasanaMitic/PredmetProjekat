using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IProductService
    {
        Guid AddProduct(ProductDto productDto);
        IEnumerable<ProductDto> GetProducts();
        bool DeleteProduct(Guid id);
    }
}
