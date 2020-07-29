using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            
            _service = service;
        }

        //GET api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItemById(Guid id)
        {
            var todoItem = _service.GetTodoItemById(id); //Why FirstOrDefaultAsync doesn't work?

            if(todoItem == null)
            {
                return NotFound();
            }

            return todoItem;    //Automatically serialized to the JSON because it is ActionResult<> type
        }

        //GET api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = _service.GetAllTodoItems().ToList();
            return todoItems;
        }

        //POST api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItem)
        {
            _service.CreateTodoItem(todoItem);

            return CreatedAtAction(nameof(GetTodoItemById), new { id = todoItem.Id }, todoItem);
        }

        //PUT api/TodoItems/5
        [HttpPut]
        public async Task<ActionResult<TodoItem>> UpdateTodoItem(Guid id, TodoItem todoItem)
        {
            if(todoItem.Id != id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //api//TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(Guid id)
        {
            var todoItem = _service.GetTodoItemById(id);

            if(todoItem == null)
            {
                return NotFound();
            }

            _service.DeleteTodoItem(todoItem);

            return NoContent();
        }

        private bool TodoItemExists(Guid id)
        {
            return _service.GetAllTodoItems().Any(e => e.Id == id);
        }
    }
}
