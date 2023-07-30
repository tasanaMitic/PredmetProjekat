using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Guid AddCategory(CategoryDto categoryDto)
        {
            var id = Guid.NewGuid();
            _unitOfWork.CategoryRepository.Add(new Category 
            { 
                CategoryId = id,
                Name = categoryDto.Name
            });

            return id;
        }

        public bool DeleteCategory(Guid id)
        { 
            return _unitOfWork.CategoryRepository.Remove(id);
        }

        public IEnumerable<CategoryDtoId> GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDtoId>>(categories);

            //return _unitOfWork.CategoryRepository.GetAll().Select(x => new CategoryDtoId    //TODO AUTOMAPPER
            //{
            //    CategoryId = x.CategoryId,
            //    Name = x.Name
            //});
        }
    }
}
