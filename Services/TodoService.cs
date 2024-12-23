﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Data;
using TodoApplikasjonAPIEntityDelTre.Services;
using TodoApplikasjonAPIEntityDelTre.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoDataRepository _todoDataRepository;

        public TodoService(ITodoDataRepository todoDataRepository)
        {
            _todoDataRepository = todoDataRepository;
        }


        public List<Todo> FetchAllTodos() => _todoDataRepository.GetAllTodos().ToList();


        public Todo FindTodoById(int id) => _todoDataRepository.GetTodoById(id);


       

        public void AddNewTodo(Todo todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo), "Todo cannot be null.");
            }

            _todoDataRepository.AddTodo(todo);  // Add the new Todo to the database
            
        }




        public void ModifyTodo(int id, Todo updatedNewTodo)
        {
            //finn id
            var todo = _todoDataRepository.GetTodoById(id);
            if (todo == null)
            {
                throw new KeyNotFoundException("Todo not found");

            }

            todo.Title = updatedNewTodo.Title;
            todo.Description = updatedNewTodo.Description;
            todo.IsCompleted = updatedNewTodo.IsCompleted;
            _todoDataRepository.UpdateTodo(todo);

        }


        public void RemoveTodo(int id)
        {
            
            _todoDataRepository.DeleteTodo(id);
            

        }


        //////
        ///
        // New method to retrieve tasks by category
        public List<Todo> GetTodosByCategory(int categoryId)
        {
            return _todoDataRepository.GetTodosByCategory(categoryId)
                .Include(t => t.Category)
                .ToList();
        }

        // New method to count tasks by category
        public int GetTodoCountByCategory(int categoryId)
        {
            return _todoDataRepository.GetTodoCountByCategory(categoryId);
        }

        // New method to retrieve completed tasks with their categories
        public List<Todo> GetCompletedTodosWithCategory()
        {
            return _todoDataRepository.GetCompletedTodosWithCategory()
                .Include(t => t.Category)
                .ToList();
        }





    }
}

   