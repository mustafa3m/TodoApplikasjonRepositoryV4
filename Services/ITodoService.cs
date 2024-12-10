using TodoApplikasjonAPIEntityDelTre.Models;

namespace TodoApplikasjonAPIEntityDelTre.Services
{
    public interface ITodoService
    {
        // Fetches all todo items
        List<Todo> FetchAllTodos();

        // Finds a specific todo item by its ID
        Todo FindTodoById(int id);

        // Adds a new todo item
        void AddNewTodo(Todo todo);
        

        // Modifies an existing todo item
        void ModifyTodo(int id, Todo updatedTodo);

        // Removes a todo item by its ID
        void RemoveTodo(int id);
    }






}
