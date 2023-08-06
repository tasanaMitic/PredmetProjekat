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
        void DeleteProduct(Guid id);
        void StockProduct(Guid productId, int quantity);
        void SellProduct(SaleDto saleDto, string username);
    }
}
