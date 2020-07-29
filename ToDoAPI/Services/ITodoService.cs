using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAllTodoItems();
        TodoItem GetTodoItemById(Guid id);
        void CreateTodoItem(TodoItem todoItem);
        void UpdateTodoItem(TodoItem todoItem);
        void DeleteTodoItem(TodoItem todoItem);
    }
}
