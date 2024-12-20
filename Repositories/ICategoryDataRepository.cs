using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Repositories
{
    public interface ICategoryDataRepository
    {
        IQueryable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);  
    }
}
