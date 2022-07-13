using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoAPI.Data.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        //// GET: api/TodoItems
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        //{
        //    return await _context.TodoItems
        //        .Select(x => ItemToDTO(x))
        //        .ToListAsync();
        //}

        //// GET: api/TodoItems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        //{
        //    var todoItem = _todoRepository.GetByID(Guid.NewGuid());

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return ItemToDTO(todoItem);
        //}
        //// PUT: api/TodoItems/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTodoItem(long id, TodoItem todoItemDTO)
        //{
        //    if (id != todoItemDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var todoItem = await _context.TodoItems.FindAsync(id);
        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    todoItem.Name = todoItemDTO.Name;
        //    todoItem.IsComplete = todoItemDTO.IsComplete;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
        //// POST: api/TodoItems
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TodoItem>> CreateTodoItem(TodoItem todoItemDTO)
        //{
        //    var todoItem = new TodoItem
        //    {
        //        IsComplete = todoItemDTO.IsComplete,
        //        Name = todoItemDTO.Name
        //    };

        //    _context.TodoItems.Add(todoItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(
        //        nameof(GetTodoItem),
        //        new { id = todoItem.Id },
        //        ItemToDTO(todoItem));
        //}

        //// DELETE: api/TodoItems/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodoItem(long id)
        //{
        //    var todoItem = await _context.TodoItems.FindAsync(id);

        //    if (todoItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TodoItems.Remove(todoItem);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TodoItemExists(long id)
        //{
        //    return _context.TodoItems.Any(e => e.Id == id);
        //}

        private static TodoItem ItemToDTO(TodoItem todoItem) =>
            new TodoItem
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
