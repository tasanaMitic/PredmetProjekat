using PredmetProjekat.Common.Dtos;

namespace PredmetProjekat.Common.Interfaces.IService
{
    public interface ICategoryService
    {
        CategoryDtoId AddCategory(CategoryDto categoryDto);
        IEnumerable<CategoryDtoId> GetCategories();
        IEnumerable<CategoryDtoId> DeleteCategory(Guid id);
    }
}
