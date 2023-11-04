using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Dtos;
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
        private readonly UserManager<Account> _userManager;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Account> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
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

        public IEnumerable<StockedProductDto> DeleteProduct(Guid id)
        {
            var productToBeDeleted = _unitOfWork.ProductRepository.GetProductById(id);
            _unitOfWork.ProductRepository.DeleteProduct(productToBeDeleted);
            _unitOfWork.SaveChanges();

            return GetProducts();
        }

        public StockedProductDto GetProduct(Guid id)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(id);
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

        public void SellProduct(SaleDto saleDto, string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                throw new KeyNotFoundException($"Employee with username: {username} not found in the database!");
            }

            foreach(var obj in saleDto.SoldProducts)
            {
                var product = _unitOfWork.ProductRepository.GetProductById(obj.ProductId);
                if(product.Quantity - obj.Quantity >= 0)
                {
                    product.Quantity -= obj.Quantity;

                    if (product.Quantity - obj.Quantity == 0)
                    {
                        product.IsInStock = false;
                    }
                }
                _unitOfWork.ProductRepository.UpdateProduct(product);
            }

            //create receipt
            var receipt = new Receipt
            {
                Date = DateTime.Now,
                ReceiptId = Guid.NewGuid(),
                SoldBy = user,
                SoldProducts = _mapper.Map<IEnumerable<SoldProduct>>(saleDto.SoldProducts),
                Register = _unitOfWork.RegisterRepository.GetRegisterById(saleDto.RegisterId)
            };

            _unitOfWork.ReceiptRepository.CreateReceipt(receipt);

            _unitOfWork.SaveChanges();
        }

        public IEnumerable<StockedProductDto> StockProduct(Guid productId, int quantity)
        {
            var product = _unitOfWork.ProductRepository.GetProductById(productId);
            product.Quantity += quantity;
            product.IsInStock = true;

            _unitOfWork.ProductRepository.UpdateProduct(product);
            _unitOfWork.SaveChanges();

            return GetProducts();
        }
    }
}
