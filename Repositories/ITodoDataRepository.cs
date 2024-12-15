using TodoApplikasjonAPIEntityDelTre.Models;
using TodoApplikasjonAPIEntityDelTre.Repositories;

namespace TodoApplikasjonAPIEntityDelTre.Repositories
{
    public interface ITodoDataRepository
    {
        IQueryable<Todo> GetAllTodos();
        Todo GetTodoById(int id);
        void AddTodo(Todo todo);
        void UpdateTodo(Todo todo);
        void DeleteTodo(int id);

        // New methods for advanced filtering
        IQueryable<Todo> GetTodosByCategory(int categoryId);
        int GetTodoCountByCategory(int categoryId);
        IQueryable<Todo> GetCompletedTodosWithCategory();
    }

}
