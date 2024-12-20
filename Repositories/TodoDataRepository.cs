using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Repositories
{
    public class TodoDataRepository : ITodoDataRepository
    {
        private readonly TodoDbContext _context;

        public TodoDataRepository(TodoDbContext context)
        {
            _context = context;
        }

        public IQueryable<Todo> GetAllTodos()
        {
            return _context.Todos
                .Include(t => t.Category);  // Load the associated category

        }

        


        public void AddTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo), "Todo cannot be null.");
            }

            // Check if the category with the given ID exists
            var category = _context.Categories.FirstOrDefault(c => c.Id == todo.CategoryId);
            if (category == null)
            {
                // If the category does not exist, throw an exception or handle it differently
                throw new ArgumentException($"Category with ID {todo.CategoryId} does not exist.");
            }

            // Link the category to the Todo
            todo.Category = category;

            // Add the Todo to the context
            _context.Todos.Add(todo);

            // Save the changes to the database
            _context.SaveChanges();
        }




        public Todo GetTodoById(int id)
        {
            return _context.Todos
                .Include(t => t.Category)
                .FirstOrDefault(b => b.Id == id) ?? new Todo();
        }

        public void UpdateTodo(Todo todo)
        {
            _context.Todos.Update(todo); // Mark the entity as modified for updating
            _context.SaveChanges();      // Apply changes to the database
        }

        public void DeleteTodo(int id)
        {
            var deleteTodo = GetTodoById(id);
            if (deleteTodo != null)
            {
                _context.Todos.Remove(deleteTodo);
                _context.SaveChanges();
            }
        }

        // Filtrage
        // Fetches todos filtered by category
        public IQueryable<Todo> GetTodosByCategory(int categoryId)
        {
            return _context.Todos.Where(t => t.CategoryId == categoryId);
        }

        // Counts todos by category
        public int GetTodoCountByCategory(int categoryId)
        {
            return _context.Todos.Count(t => t.CategoryId == categoryId);
        }

        // Fetches completed todos with their categories
        public IQueryable<Todo> GetCompletedTodosWithCategory()
        {
            return _context.Todos.Where(t => t.IsCompleted == true);
        }
    }
}
