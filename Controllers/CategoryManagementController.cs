using Microsoft.AspNetCore.Mvc;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Services;

namespace TodoApplikasjonAPIEntityDelTre.Controllers
{
    [ApiController]  
    [Route("api/[controller]")]
    public class CategoryManagementController : ControllerBase
    {
        private readonly ICategoryService _CategoryService;

        public CategoryManagementController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _CategoryService.FetchAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _CategoryService.FindCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                Console.WriteLine("Received null category.");
                return BadRequest("Category cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalid: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));
                return BadRequest(ModelState);
            }

            _CategoryService.AddNewCategory(category);
            Console.WriteLine($"Category {category.Name} created with ID {category.Id}.");
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }



        

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
                return BadRequest();

            _CategoryService.ModifyCategory(id,category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _CategoryService.RemoveCategory(id);
            return NoContent();
        }
    }
}
