using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            return _mapper.Map<StockedProductDtoId>(product);
        }

        public IEnumerable<StockedProductDtoId> GetProducts()
        {
            var stockedProducts = _unitOfWork.ProductRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<StockedProductDtoId>>(stockedProducts);
        }

        public IEnumerable<StockedProductDtoId> GetStockedProducts()
        {
            var stockedProducts = _unitOfWork.ProductRepository.GetAllStockedProducts();
            return _mapper.Map<IEnumerable<StockedProductDtoId>>(stockedProducts);
        }

        public bool SellProduct(IEnumerable<ProductDtoId> products)
        {
            //todo
            throw new NotImplementedException();
        }

        public void StockProduct(Guid productId, int quantity)
        {
            var product = _unitOfWork.ProductRepository.GetById(productId);
            product.Quantity += quantity;
            product.IsInStock = true;

            _unitOfWork.ProductRepository.Update(product);
        }
    }
}
