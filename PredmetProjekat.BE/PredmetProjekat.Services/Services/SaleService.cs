using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PredmetProjekat.Common.Constants;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;
using System.Globalization;

namespace PredmetProjekat.Services.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;
        private readonly IDocumentService _documentService;
        public SaleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Account> userManager, IDocumentService documentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _documentService = documentService;
        }

        public void CreatePDF(FilterParams filterParams, string username)
        {
            var sales = GetSalesByFilter(filterParams, username);
            _documentService.CreatePDF(sales, filterParams, username);
        }

        public IEnumerable<ReceiptDto> GetAllSales()
        {
            var receipts = _unitOfWork.ReceiptRepository.GetAllReceipts();
            return _mapper.Map<IEnumerable<ReceiptDto>>(receipts);
        }

        public IEnumerable<ReceiptDto> GetAllSalesForUser(string username)
        {
            var user = GetUser(username);

            var sales = _unitOfWork.ReceiptRepository.GetAllReceiptsForUser(user);
            return _mapper.Map<IEnumerable<ReceiptDto>>(sales);
        }

        public FilterSearchDto GetFilteredSales(FilterParams filterParams, string username)
        {
            var sales = GetSalesByFilter(filterParams, username);
            var user = GetUser(username);

            return new FilterSearchDto()
            {
                ReceiptDtos = _mapper.Map<IEnumerable<ReceiptDto>>(sales), 
                OptionParameters = GetFilterOptions(user, IsUserAnEmployee(user))
            };
        }

        private IEnumerable<Receipt> GetSalesByFilter(FilterParams filterParams, string username)
        {
            var registerCodes = filterParams?.RegisterCodes?.Split(',') ?? null;
            var locations = filterParams?.Locations?.Split(',') ?? null;
            var startDate = filterParams?.StartDate;
            var endDate = filterParams?.EndDate;
            var price = filterParams?.Price;

            if (startDate != null && endDate != null && (DateTime.Parse(startDate, CultureInfo.InvariantCulture) > DateTime.Parse(endDate, CultureInfo.InvariantCulture)))
            {
                throw new Exception($"End date cannot be before start date!");
            }

            var user = GetUser(username);
            var employees = IsUserAnEmployee(user) ? new[] { username } : filterParams?.EmployeeUsernames?.Split(',');

            var sales = _unitOfWork.ReceiptRepository.GetFilteredSales(employees, registerCodes, locations, startDate, endDate, price);

            return OrderSales(sales, filterParams?.OrderBy);
        } 

        private IEnumerable<Receipt> OrderSales(IEnumerable<Receipt> sales, int? orderType)
        {
            switch (orderType)
            {
                //Oldest first
                case 1:
                    return sales.OrderBy(x => x.Date);

                //Latest first
                case 2:
                    return sales.OrderByDescending(x => x.Date);

                //Employee A-Z
                case 3:
                    return sales.OrderBy(x => x.SoldBy.UserName);

                //Employee Z-A
                case 4:
                    return sales.OrderByDescending(x => x.SoldBy.UserName);

                //Price (Low to High)
                case 5:
                    return sales.OrderBy(x => x.TotalPrice);

                //Price (High to Low)
                case 6:
                    return sales.OrderByDescending(x => x.TotalPrice);

                default:
                    return sales;
            }
        }

        private OptionParams GetFilterOptions(Account user, bool isEmployee)
        {
            IEnumerable<Receipt> allSales;

            if (isEmployee)
            {
                allSales = _unitOfWork.ReceiptRepository.GetAllReceiptsForUser(user);
            }
            else
            {
                allSales = _unitOfWork.ReceiptRepository.GetAllReceipts();
            }

            return new OptionParams()
            {
                EmployeeUsernames = allSales.Select(x => x.SoldBy.UserName).Distinct().ToList(),
                Locations = allSales.Select(x => x.Register.Location).Distinct().ToList(),
                RegisterCodes = allSales.Select(x => x.Register.RegisterCode).Distinct().ToList()
            };
        }

        public void SellProduct(SaleDto saleDto, string username)
        {
            var user = GetUser(username);

            decimal totalPrice = 0;
            foreach (var obj in saleDto.SoldProducts)
            {
                var product = _unitOfWork.ProductRepository.GetProductById(obj.ProductId);
                if (product.Quantity - obj.Quantity >= 0 && product.IsInStock && !product.IsDeleted)
                {
                    product.Quantity -= obj.Quantity;
                    totalPrice += product.Price * obj.Quantity;

                    if (product.Quantity == 0)
                    {
                        product.IsInStock = false;
                    }
                }
                _unitOfWork.ProductRepository.UpdateProduct(product);
            }

            var soldProductIds = CreateSoldProducts(saleDto.SoldProducts).ToList();
            _unitOfWork.SaveChanges();

            var soldProducts = _unitOfWork.SoldProductRepository.GetSoldProductsByIds(soldProductIds);


            var receipt = new Receipt
            {
                Date = DateTime.Now,
                ReceiptId = Guid.NewGuid(),
                SoldBy = user,
                SoldProducts = soldProducts,
                Register = _unitOfWork.RegisterRepository.GetRegisterById(saleDto.RegisterId),
                TotalPrice = Math.Round(totalPrice, 2)
            };

            _unitOfWork.ReceiptRepository.CreateReceipt(receipt);
            _unitOfWork.SaveChanges();
        }


        private IEnumerable<Guid> CreateSoldProducts(IEnumerable<SoldProductDto> soldProducts)
        {
            List<Guid> soldProductIds = new List<Guid>();
            foreach (SoldProductDto dto in soldProducts)
            {
                var id = Guid.NewGuid();
                var soldProduct = new SoldProduct
                {
                    SoldProductId = id,
                    Product = _unitOfWork.ProductRepository.GetProductById(dto.ProductId),
                    Quantity = dto.Quantity
                };
                _unitOfWork.SoldProductRepository.CreateSoldProduct(soldProduct);
                soldProductIds.Add(id);
            }
            return soldProductIds;

        }

        private Account GetUser(string username)
        {
            var user = _userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                throw new KeyNotFoundException($"Employee with username: {username} not found in the database!");
            }
            return user;
        }

        private bool IsUserAnEmployee(Account user)
        {
            return _userManager.IsInRoleAsync(user, Constants.EmployeeRole).Result;
        }
    }
}
