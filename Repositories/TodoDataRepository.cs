﻿using TodoApplikasjonAPIEntityDelTre.Data;
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
            // Return the Todo with its associated Category
            var todos = _context.Todos
                .Include(t => t.Category)  // Load the associated category
                .Select(t => new Todo
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    CategoryId = t.CategoryId,
                    Category = new Category
                    {
                        Id = t.Category.Id,
                        Name = t.Category.Name
                    }
                });

            return todos;
        }




        //public IQueryable<Todo> GetAllTodos()
        //{
        //    return _context.Todos
        //       .Include(t => t.Category)
        //       .ThenInclude(c => c.Todos);


        //}
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
