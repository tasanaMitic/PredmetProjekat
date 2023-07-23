using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid AddProduct(ProductDto productDto)
        {
            var id = Guid.NewGuid();
            _unitOfWork.ProductRepository.Add(new Product
            {
                ProductId = id, 
                Name = productDto.Name,
                Size = productDto.Size,
                Season = productDto.Season,
                Sex = productDto.Sex,
                Quantity = 0,
                Brand = _unitOfWork.BrandRepository.GetById(productDto.BrandId),
                Category = _unitOfWork.CategoryRepository.GetById(productDto.CategoryId),
                IsInStock = false
            });

            return id;
        }

        public bool DeleteProduct(Guid id)
        {
            return _unitOfWork.ProductRepository.Remove(id);
        }

        public StockedProductDtoId GetProduct(Guid id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            
            return new StockedProductDtoId
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Season = product.Season,
                Sex = product.Sex,
                Size = product.Size,
                Quantity = product.Quantity,
                BrandId = product.Brand.BrandId,
                CategoryId = product.Category.CategoryId
            };
        }

        public IEnumerable<StockedProductDtoId> GetProducts()
        {
            return _unitOfWork.ProductRepository.GetAll().Select(x => new StockedProductDtoId
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Season = x.Season,
                Size = x.Size,
                Sex = x.Sex,
                Quantity = x.Quantity,
                BrandId = x.Brand.BrandId,
                CategoryId = x.Category.CategoryId
            });
        }

        public bool SellProduct(IEnumerable<ProductDtoId> products)
        {
            //todo
            throw new NotImplementedException();
        }

        public Guid StockProduct(ProductDtoId productDto, int quantity)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
