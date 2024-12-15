using TodoApplikasjonAPIEntityDelTre.Data;
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
               .Include(t => t.Category);

            //return _context.Todos.ToList();

            //.Include(t => t.Category); 
        }
        public void AddTodo(Todo todo)
        {
                if (todo == null)
                {
                    throw new ArgumentNullException(nameof(todo), "Todo cannot be null.");
                }
            // Add the validated Todo object to the database
            _context.Todos.Add(todo);
            // Save changes to persist the Todo object in the database
             _context.SaveChanges();

        }



        //public void AddTodo(Todo todo)
        //{
        //    // Check if the 'todo' object is null
        //    if (todo == null)
        //    {
        //        throw new ArgumentNullException(nameof(todo), "Todo cannot be null.");
        //    }

        //    // Check if the Title is provided (non-null and not empty)
        //    if (string.IsNullOrWhiteSpace(todo.Title))
        //    {
        //        throw new ArgumentException("Title cannot be null or empty.");
        //    }

        //    // Check if the CategoryId is valid (assuming it must be a positive integer)
        //    if (todo.CategoryId <= 0)
        //    {
        //        throw new ArgumentException("CategoryId must be a positive integer.");
        //    }

        //    // Check if the Description does not exceed a maximum length (optional validation)
        //    if (todo.Description?.Length > 500) // Assuming 500 characters max for Description
        //    {
        //        throw new ArgumentException("Description cannot exceed 500 characters.");
        //    }

        //    // Add the validated Todo object to the database
        //    _context.Todos.Add(todo);

        //    try
        //    {
        //        // Save changes to persist the Todo object in the database
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Handle any database-specific errors
        //        throw new InvalidOperationException("An error occurred while saving the Todo.", ex);
        //    }
        //}



        public Todo GetTodoById(int id)
        {
            return _context.Todos
                
                .Include(t => t.Category)
                .FirstOrDefault(b => b.Id == id) ?? new Todo();

        }

        public void UpdateTodo(Todo todo)
        {
            _context.Todos.Update(todo); // Marque l'entité book comme modifiée pour la mise à jour
            _context.SaveChanges();      // Applique les changements à la base de données
        }


        public void DeleteTodo(int id)
        { 
            var deleteBook = GetTodoById(id);
            if (deleteBook != null)
            {
                _context.Todos.Remove(deleteBook);
                _context.SaveChanges();

            }

        }

        //Filtrage
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
