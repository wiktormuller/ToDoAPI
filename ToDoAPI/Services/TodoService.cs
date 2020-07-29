using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public void CreateTodoItem(TodoItem todoItem)
        {
            if(todoItem == null)
            {
                throw new ArgumentNullException(nameof(todoItem));
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public void DeleteTodoItem(TodoItem todoItem)
        {
            if(todoItem == null)
            {
                throw new ArgumentNullException(nameof(todoItem));
            }

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            var todoItems = _context.TodoItems;

            return todoItems;
        }

        public TodoItem GetTodoItemById(Guid id) //How to make it async?
        {
            var todoItem = _context.TodoItems.Find(id);

            return todoItem;
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            //Nothing -> EF Core update it's automatically ???
        }
    }
}
