
using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public BrandDtoId AddBrand(BrandDto brandDto)
        {
            var id = Guid.NewGuid();
            var brand = new Brand
            {
                BrandId = id,
                Name = brandDto.Name
            };
            _unitOfWork.BrandRepository.CreateBrand(brand);
            _unitOfWork.SaveChanges();

            return _mapper.Map<BrandDtoId>(brand); ;
        }

        public IEnumerable<BrandDtoId> DeleteBrand(Guid id)
        {
            var brandToBeDeleted = _unitOfWork.BrandRepository.GetBrandById(id);
            _unitOfWork.BrandRepository.DeleteBrand(brandToBeDeleted);
            _unitOfWork.SaveChanges();

            return GetBrands();
        }

        public IEnumerable<BrandDtoId> GetBrands()
        {
            var brands = _unitOfWork.BrandRepository.GetAllBrands();
            return _mapper.Map<IEnumerable<BrandDtoId>>(brands);
        }
    }
}
