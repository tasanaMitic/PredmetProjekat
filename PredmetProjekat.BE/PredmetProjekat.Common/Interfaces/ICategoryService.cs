using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces
{
    public interface ICategoryService
    {
        Guid AddCategory(CategoryDto categoryDto);
        IEnumerable<CategoryDto> GetCategories();
        bool DeleteCategory(Guid id);
    }
}
