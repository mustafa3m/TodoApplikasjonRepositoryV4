using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Repositories
{
    public class CategoryDataRepository : ICategoryDataRepository
    {
        private readonly TodoDbContext _context;

        public CategoryDataRepository(TodoDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Id == id) ?? new Category();
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}

