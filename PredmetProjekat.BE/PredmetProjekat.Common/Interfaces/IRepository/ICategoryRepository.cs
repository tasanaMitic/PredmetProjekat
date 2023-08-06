using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.Interfaces.IRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(Guid categoryId);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
