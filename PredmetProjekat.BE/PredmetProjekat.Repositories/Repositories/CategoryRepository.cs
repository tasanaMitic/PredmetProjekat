using PredmetProjekat.Common.Interfaces.IRepository;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;

namespace PredmetProjekat.Repositories.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext context) : base(context)
        {
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return GetAll();
        }

        public Category GetCategoryById(Guid categoryId)
        {
            return GetById(categoryId);
        }
    }
}
