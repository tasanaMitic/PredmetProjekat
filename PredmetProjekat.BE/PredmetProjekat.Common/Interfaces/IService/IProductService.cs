using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.ProductDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IProductService
    {
        Guid AddProduct(ProductDto productDto);
        IEnumerable<StockedProductDto> GetProducts();
        IEnumerable<StockedProductDto> GetStockedProducts();
        StockedProductDto GetProduct(Guid id);
        IEnumerable<StockedProductDto> DeleteProduct(Guid id);
        IEnumerable<StockedProductDto> StockProduct(Guid productId, int quantity);
        void SellProduct(SaleDto saleDto, string username);
    }
}
