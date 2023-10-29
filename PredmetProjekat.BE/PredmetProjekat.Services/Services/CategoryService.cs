using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
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
        public CategoryDtoId AddCategory(CategoryDto categoryDto)
        {
            var id = Guid.NewGuid();
            var category = new Category
            {
                CategoryId = id,
                Name = categoryDto.Name
            };
            _unitOfWork.CategoryRepository.CreateCategory(category);
            _unitOfWork.SaveChanges();

            return _mapper.Map<CategoryDtoId>(category);
        }

        public IEnumerable<CategoryDtoId> DeleteCategory(Guid id)
        {
            var categoryToBeDeleted = _unitOfWork.CategoryRepository.GetCategoryById(id);
            _unitOfWork.CategoryRepository.DeleteCategory(categoryToBeDeleted);
            _unitOfWork.SaveChanges();
            //todo handle products

            return GetCategories();
        }

        public IEnumerable<CategoryDtoId> GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryDtoId>>(categories);
        }
    }
}
