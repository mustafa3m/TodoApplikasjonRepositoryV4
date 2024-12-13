using System;
using System.Collections.Generic;
using System.Linq;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using TodoApplikasjonAPIEntityDelTre.Services;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public class CategoryService : ICategoryService
    {
        

        private readonly ICategoryDataRepository _categoryDataRepository;
       

        public CategoryService(CategoryDataRepository categoryDataRepository)
        {
            _categoryDataRepository = categoryDataRepository;
        }

        public List<Category> FetchAllCategories() => _categoryDataRepository.GetAllCategories().ToList();

        public Category FindCategoryById(int id) => _categoryDataRepository.GetCategoryById(id);

        public void AddNewCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _categoryDataRepository.AddCategory(category);
        }

        public void ModifyCategory(int id, Category updatedCategory)
        {

            var existingCategory = _categoryDataRepository.GetCategoryById(id);
            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;
                _categoryDataRepository.UpdateCategory( existingCategory);
                
            }

            
        }

        public void RemoveCategory(int id)
        {


            _categoryDataRepository.DeleteCategory(id);
        }
    }
}
