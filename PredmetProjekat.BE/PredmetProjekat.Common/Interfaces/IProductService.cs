using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface IProductService
    {
        Guid AddProduct(ProductDto productDto);
        IEnumerable<StockedProductDtoId> GetProducts();
        IEnumerable<StockedProductDtoId> GetStockedProducts();
        StockedProductDtoId GetProduct(Guid id);
        bool DeleteProduct(Guid id);
        void StockProduct(Guid productId, int quantity);
        bool SellProduct(IEnumerable<ProductDtoId> products);
    }
}
