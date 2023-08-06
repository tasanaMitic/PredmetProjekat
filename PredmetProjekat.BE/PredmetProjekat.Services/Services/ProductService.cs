using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
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
            _unitOfWork.ProductRepository.CreateProduct(new Product
            {
                ProductId = id,
                Name = productDto.Name,
                Size = productDto.Size,
                Season = productDto.Season,
                Sex = productDto.Sex,
                Quantity = 0,
                Brand = _unitOfWork.BrandRepository.GetBrandById(productDto.BrandId),
                Category = _unitOfWork.CategoryRepository.GetCategoryById(productDto.CategoryId),
                IsInStock = false
            });
            _unitOfWork.SaveChanges();

            return id;
        }

        public void DeleteProduct(Guid id)
        {
            var productToBeDeleted = _unitOfWork.ProductRepository.GetProductById(id);
            _unitOfWork.ProductRepository.DeleteProduct(productToBeDeleted);
            _unitOfWork.SaveChanges();
        }

        public StockedProductDtoId GetProduct(Guid id)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(id);
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
