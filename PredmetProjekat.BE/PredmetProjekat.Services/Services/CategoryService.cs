using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _unitOfWork.CategoryRepository.GetAll().Select(x => new CategoryDtoId
            {
                CategoryId = x.CategoryId,
                Name = x.Name
            });
        }
    }
}
