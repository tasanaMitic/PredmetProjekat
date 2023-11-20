using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.ProductDtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IProductService
    {
        string AddProduct(ProductDto productDto);
        IEnumerable<StockedProductDto> GetProducts();
        IEnumerable<StockedProductDto> GetStockedProducts();
        StockedProductDto GetProduct(string productId);
        IEnumerable<StockedProductDto> DeleteProduct(string productId);
        IEnumerable<StockedProductDto> StockProduct(string productId, int quantity);
        IEnumerable<StockedProductDto> UpdateProductPrice(string productId, decimal price);
        void SellProduct(SaleDto saleDto, string username);
        IEnumerable<ReceiptDto> GetAllSalesForUser(string username);
        IEnumerable<ReceiptDto> GetAllSales();
    }
}
