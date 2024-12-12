using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using TodoApplikasjonAPIEntityDelTre.Services;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public class TodoService : ITodoService
    {
        public readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;

        }

        public List<Todo> FetchAllTodos() => _context.Todos.ToList();
        

        public Todo FindTodoById(int id) => _context.Todos.Find(id);


        //public void AddNewTodo(Todo todo)
        //{
        //    _context.Todos.Add(todo);
        //    _context.SaveChanges();

        //}

        public void AddNewTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo), "Todo cannot be null.");
            }

            _context.Todos.Add(todo);  // Add the new Todo to the database
            _context.SaveChanges();    // Save the changes to the database
        }




        public void ModifyTodo(int id, Todo updatedNewTodo)
        {
            //finn id
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                throw new KeyNotFoundException("Todo not found");

            }

            todo.Title = updatedNewTodo.Title;
            todo.Description = updatedNewTodo.Description;
            todo.IsCompleted = updatedNewTodo.IsCompleted;
            _context.SaveChanges();

        }


        public void RemoveTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();

        }
    }
}

   