using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Services;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoManagementController : ControllerBase
    {
        private readonly ITodoService _todoService;

        // Constructor with dependency injection for the service
        public TodoManagementController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        ///// <summary>
        ///// Fetch all Todos.
        ///// </summary>
        [HttpGet]
        public IActionResult GetAllTodos()
        {
            var todos = _todoService.FetchAllTodos();
            return Ok(todos);
            
        }



        /// <summary>
        /// Fetch a specific Todo by ID.
        /// </summary>
        /// <param name="id">The ID of the Todo to fetch.</param>
        [HttpGet("{id}")]
        public IActionResult GetTodoById(int id)
        {
            var todo = _todoService.FindTodoById(id);
            if (todo == null)
            {
                return NotFound(new { message = $"Todo with ID {id} not found." });
            }

            return Ok(todo);
        }

        ///// <summary>
        ///// Create a new Todo item.
        ///// </summary>
        ///// <param name="todo">The Todo item to create.</param>
    
        [HttpPost]
        public IActionResult CreateTodo(Todo todo)
        {
            try
            {
                _todoService.AddNewTodo(todo);
                return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
            }
            catch (ArgumentException ex)
            {
                // Retourner une erreur 400 si la catégorie n'existe pas
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Update an existing Todo item.
        /// </summary>
        /// <param name="id">The ID of the Todo to update.</param>
        /// <param name="newTodo">The updated Todo object.</param>
        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] Todo newTodo)
        {
            // Ensure the route ID matches the Todo ID in the body
            if (id != newTodo.Id)
            {
                return BadRequest("The ID in the route does not match the ID of the Todo.");
            }

            try
            {
                _todoService.ModifyTodo(id, newTodo);
                return NoContent(); // 204 No Content indicates successful update
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Todo with ID {id} not found." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Todo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Delete a Todo item.
        /// </summary>
        /// <param name="id">The ID of the Todo to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            try
            {
                _todoService.RemoveTodo(id);
                return Ok(new { message = $"Todo with ID {id} has been deleted." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Todo with ID {id} not found." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Todo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        // GET: api/todos/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public ActionResult<List<Todo>> GetTodosByCategory(int categoryId)
        {
            var todos = _todoService.GetTodosByCategory(categoryId);
            if (todos == null || !todos.Any())
            {
                return NotFound("No todos found in this category");
            }
            return Ok(todos);
        }

        // GET: api/todos/count/category/{categoryId}
        [HttpGet("count/category/{categoryId}")]
        public ActionResult<int> GetTodoCountByCategory(int categoryId)
        {
            int count = _todoService.GetTodoCountByCategory(categoryId);
            return Ok(count);
        }

        // GET: api/todos/completed
        [HttpGet("completed")]
        public ActionResult<List<Todo>> GetCompletedTodosWithCategory()
        {
            var todos = _todoService.GetCompletedTodosWithCategory();
            return Ok(todos);
        }
    }
}

