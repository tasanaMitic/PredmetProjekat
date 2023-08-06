using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface ICategoryService
    {
        Guid AddCategory(CategoryDto categoryDto);
        IEnumerable<CategoryDtoId> GetCategories();
        void DeleteCategory(Guid id);
    }
}
