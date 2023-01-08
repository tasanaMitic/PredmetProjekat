using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;

namespace PredmetProjekat.Services.Services
{
    public class ProductService : IProductService
    {
        public Guid AddProduct(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductDtoId GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            throw new NotImplementedException();
        }

        public bool SellProduct(IEnumerable<ProductDtoId> products)
        {
            throw new NotImplementedException();
        }

        public Guid StockProduct(ProductDtoId productDto, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
