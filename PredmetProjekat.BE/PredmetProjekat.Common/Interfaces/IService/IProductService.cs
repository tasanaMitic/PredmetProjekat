using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface IProductService
    {
        Guid AddProduct(ProductDto productDto);
        IEnumerable<StockedProductDtoId> GetProducts();
        IEnumerable<StockedProductDtoId> GetStockedProducts();
        StockedProductDtoId GetProduct(Guid id);
        void DeleteProduct(Guid id);
        void StockProduct(Guid productId, int quantity);
        bool SellProduct(IEnumerable<ProductDtoId> products);
    }
}
