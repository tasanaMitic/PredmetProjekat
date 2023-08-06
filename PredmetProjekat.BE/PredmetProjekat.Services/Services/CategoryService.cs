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
        public Guid AddCategory(CategoryDto categoryDto)
        {
            var id = Guid.NewGuid();
            _unitOfWork.CategoryRepository.CreateCategory(new Category 
            { 
                CategoryId = id,
                Name = categoryDto.Name
            });
            _unitOfWork.SaveChanges();

            return id;
        }

        public void DeleteCategory(Guid id)
        {
            var categoryToBeDeleted = _unitOfWork.CategoryRepository.GetCategoryById(id);
            _unitOfWork.CategoryRepository.DeleteCategory(categoryToBeDeleted);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<CategoryDtoId> GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryDtoId>>(categories);
        }
    }
}
