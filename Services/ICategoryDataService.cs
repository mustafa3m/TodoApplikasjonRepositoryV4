using TodoApplikasjonAPIEntityDelTre.Models;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public interface ICategoryDataService
    {
        // Fetches all categories
        List<Category> FetchAllCategories();

        // Finds a specific category by its ID
        Category FindCategoryById(int id);

        // Adds a new category
        void AddNewCategory(Category category);

        // Modifies an existing category
        void ModifyCategory(int id, Category updatedCategory);

        // Removes a category by its ID
        void RemoveCategory(int id);

        
    }
}

