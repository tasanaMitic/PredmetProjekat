using AutoMapper;
using PredmetProjekat.Common.Dtos.ProductDtos;
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

        public string AddProduct(ProductDto productDto)
        {
            var brand = _unitOfWork.BrandRepository.GetBrandById(productDto.BrandId);
            var category = _unitOfWork.CategoryRepository.GetCategoryById(productDto.CategoryId);
            var productType = _unitOfWork.ProductTypeRepository.GetProductTypeById(productDto.ProductTypeId);

            if (!productType.Attributes.All(attr => productDto.AttributeValues.Any(dto => dto.AttributeId == attr.AttributeId)) && (productType.Attributes.Count() != productDto.AttributeValues.Count()))
            {
                throw new Exception($"Attributes don't match to the selected product type!");
            }

            var productId = $"{productDto.Name.Replace(' ', '-')}-{brand.Name}-{category.Name}";

            _unitOfWork.ProductRepository.CreateProduct(new Product
            {
                ProductId = productId,
                Name = productDto.Name,
                Brand = brand,
                Category = category,
                ProductType = productType,
                AttributeValues = GetProductAttributes(productDto.AttributeValues, productType),
                IsInStock = false
            });
            _unitOfWork.SaveChanges();

            return productId;
        }

        public IEnumerable<StockedProductDto> DeleteProduct(string productId)
        {
            var productToBeDeleted = _unitOfWork.ProductRepository.GetProductById(productId);
            _unitOfWork.ProductRepository.DeleteProduct(productToBeDeleted);
            _unitOfWork.SaveChanges();

            return GetProducts();
        }

        public StockedProductDto GetProduct(string productId)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(productId);
            return _mapper.Map<StockedProductDto>(product);
        }

        public IEnumerable<StockedProductDto> GetProducts()
        {
            var products = _unitOfWork.ProductRepository.GetAllProducts();
            return _mapper.Map<IEnumerable<StockedProductDto>>(products);
        }

        public IEnumerable<StockedProductDto> GetStockedProducts()
        {
            var stockedProducts = _unitOfWork.ProductRepository.GetAllStockedProducts();
            return _mapper.Map<IEnumerable<StockedProductDto>>(stockedProducts);
        }


        public IEnumerable<StockedProductDto> StockProduct(string productId, int quantity)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(productId);
            product.Quantity += quantity;
            product.IsInStock = true;

            _unitOfWork.ProductRepository.UpdateProduct(product);
            _unitOfWork.SaveChanges();

            return GetProducts();
        }

        public IEnumerable<StockedProductDto> UpdateProductPrice(string productId, decimal price)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(productId);
            product.Price = price;

            _unitOfWork.ProductRepository.UpdateProduct(product);
            _unitOfWork.SaveChanges();

            return GetProducts();
        }

        private List<AttributeValue> GetProductAttributes(IEnumerable<AttributeValueDto> attributeValuesDto, ProductType productType)
        {
            var attributes = new List<AttributeValue>();

            foreach (var attributeValueDto in attributeValuesDto)
            {
                var attributeValue = new AttributeValue
                {
                    AttributeValueId = Guid.NewGuid(),
                    Value = attributeValueDto.AttributeValue,
                    ProductAttribute = productType.Attributes.Where(x => x.AttributeId == attributeValueDto.AttributeId).First()
                };
                attributes.Add(attributeValue);
            }

            return attributes;
        }

    }
}
