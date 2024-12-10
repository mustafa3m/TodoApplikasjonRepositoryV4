using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Services;

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

        [HttpPost]

        public IActionResult CreateTodo(Todo todo)
        {
            _todoService.AddNewTodo(todo);// Adds a new todo.

            return Ok();
        }



        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, Todo newTodo)
        {
            try
            {
                // Call ModifyTodo to update and save automatically in the service
                _todoService.ModifyTodo(id, newTodo);
                return NoContent();  // Return 204 No Content, indicating successful update without additional data
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  // If the item is not found, return 404 Not Found
            }
        }

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

