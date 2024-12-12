using System;
using System.Collections.Generic;
using System.Linq;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using TodoApplikasjonAPIEntityDelTre.Services;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly TodoDbContext _context;

        public CategoryService(TodoDbContext context)
        {
            _context = context;
        }

        public List<Category> FetchAllCategories() => _context.Categories.ToList();

        public Category FindCategoryById(int id) => _context.Categories.Find(id);

        public void AddNewCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void ModifyCategory(int id, Category updatedCategory)
        {

            var existingCategory = _context.Categories.Find(id);
            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                _context.SaveChanges();
            }

            
        }

        public void RemoveCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("Category not found");
            }
        }
    }
}
