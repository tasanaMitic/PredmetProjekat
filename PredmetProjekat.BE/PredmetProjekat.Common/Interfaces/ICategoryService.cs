using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface ICategoryService
    {
        Guid AddCategory(CategoryDto categoryDto);
        IEnumerable<CategoryDtoId> GetCategories();
        bool DeleteCategory(Guid id);
    }
}
