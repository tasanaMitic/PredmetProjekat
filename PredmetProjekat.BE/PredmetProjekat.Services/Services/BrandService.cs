
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Guid AddBrand(BrandDto brandDto)
        {
            var id = Guid.NewGuid();
           _unitOfWork.BrandRepository.Add(new Brand
            {
                BrandId = id,
                Name = brandDto.Name
            });

            return id;
        }

        public bool DeleteBrand(Guid id)
        {
            return _unitOfWork.BrandRepository.Remove(id);
        }

        public IEnumerable<BrandDtoId> GetBrands()
        {
            return _unitOfWork.BrandRepository.GetAll().Select(x => new BrandDtoId
            {
                BrandId = x.BrandId,
                Name = x.Name
            });
        }
    }
}
