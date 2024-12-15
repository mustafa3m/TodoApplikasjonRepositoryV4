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

        /// <summary>
        /// Create a new Todo item.
        /// </summary>
        /// <param name="todo">The Todo item to create.</param>
        [HttpPost]
        public IActionResult CreateTodo(Todo todo)
        {

            _todoService.AddNewTodo(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
            //// Check if the provided Todo object is null
            //if (todo == null)
            //{
            //    return BadRequest("The Todo object cannot be null.");
            //}

            //// Validate the model state
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //try
            //{
            //    _todoService.AddNewTodo(todo);
            //    return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error adding Todo: {ex.Message}");
            //    return StatusCode(500, "An error occurred while processing your request.");
            //}
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


/*
  using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Services;


/// <summary>
/// //
/// </summary>




namespace TodoApplikasjonAPIEntityDelTre.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TodoManagementController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoManagementController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public IActionResult GetAllTodos() => Ok(_todoService.FetchAllTodos());// Fetches all Todos.

        [HttpGet("id")]

        public IActionResult GetTodoById(int id)
        {
            var todo = _todoService.FindTodoById(id); // Fetches a todo by ID.
            if (todo == null) NotFound();

            return Ok(todo);
        }

        

       



        // POST: api/todo
        [HttpPost]
        public IActionResult CreateTodo([FromBody] Todo todo)
        {
            // Check if the todo is null
            if (todo == null)
            {
                return BadRequest("Todo cannot be null.");
            }

            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return validation errors if the model is invalid
            }

            try
            {
                // Add the new Todo
                _todoService.AddNewTodo(todo);
                // Return Created response with the created todo
                return CreatedAtAction(nameof(GetAllTodos), new { id = todo.Id }, todo);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error adding Todo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");  // Return 500 if there's a server error
            }
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] Todo newTodo)
        {
            // Check if the ID in the URL matches the one in the body
            if (id != newTodo.Id)
            {
                return BadRequest("Route ID does not match Todo ID.");
            }

            try
            {
                // Update the Todo in the service
                _todoService.ModifyTodo(id, newTodo);
                // Return 204 No Content, indicating successful update
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  // If the item is not found, return 404 Not Found
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Error updating Todo: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");  // Return 500 if there's a server error
            }
        }







        //[HttpPut("{id}")]
        //public IActionResult UpdateTodo(int id, Todo newTodo)
        //{
        //    try
        //    {
        //        // Call ModifyTodo to update and save automatically in the service
        //        _todoService.ModifyTodo(id, newTodo);
        //        return NoContent();  // Return 204 No Content, indicating successful update without additional data
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();  // If the item is not found, return 404 Not Found
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            try
            {
                // Call RemoveTodo to delete and save directly in the service
                _todoService.RemoveTodo(id);// Removes the todo
                return Ok();  // Return 200 OK
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  // If the item is not found, return 404 Not Found
            }
        }






    }
}

*/
