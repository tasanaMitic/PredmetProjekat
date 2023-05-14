using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IProductService
    {
        Guid AddProduct(ProductDto productDto);
        IEnumerable<StockedProductDtoId> GetProducts();
        StockedProductDtoId GetProduct(Guid id);
        bool DeleteProduct(Guid id);
        Guid StockProduct(ProductDtoId productDto, int quantity);
        bool SellProduct(IEnumerable<ProductDtoId> products);
    }
}
