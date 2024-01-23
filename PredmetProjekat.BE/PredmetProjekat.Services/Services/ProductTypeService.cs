using AutoMapper;
using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ProductTypeDtoId AddProductType(ProductTypeDto productTypeDto)
        {
            List<ProductAttribute> attributes = new List<ProductAttribute>();
            foreach (string attribute in productTypeDto.Attributes)
            {
                ProductAttribute productAttribute = new ProductAttribute 
                { 
                    AttributeName = attribute,
                    AttributeId = Guid.NewGuid()
                };
                attributes.Add(productAttribute);
               
            }

            var productType = new ProductType
            {
                Name = productTypeDto.Name,
                Attributes = attributes,
                ProductTypeId = Guid.NewGuid()
            };
            _unitOfWork.ProductTypeRepository.CreateProductType(productType);
            _unitOfWork.SaveChanges();

            return _mapper.Map<ProductTypeDtoId>(productType);
        }

        public IEnumerable<ProductTypeDtoId> DeleteProductTypes(Guid id)
        {
            var productTypeToDelete = _unitOfWork.ProductTypeRepository.GetProductTypeById(id);
            _unitOfWork.ProductTypeRepository.DeleteProductType(productTypeToDelete);
            _unitOfWork.SaveChanges();

            return GetProductTypes();
        }

        public IEnumerable<ProductTypeDtoId> GetProductTypes()
        {
            var productTypes = _unitOfWork.ProductTypeRepository.GetAllProductTypes();
            return _mapper.Map<IEnumerable<ProductTypeDtoId>>(productTypes);
        }
    }
}
