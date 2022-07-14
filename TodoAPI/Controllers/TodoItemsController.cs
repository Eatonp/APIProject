using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoAPI.Data.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodoItemsController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_todoRepository.GetAll());
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(Guid id)
        {
            if (!_todoRepository.Exists(id))
                return NoContent();
            
            var todoItem = _todoRepository.GetByID(id);
            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateAsync(Guid id, TodoItem todoItem)
        {
            if (!_todoRepository.Exists(id))
                return NoContent();

            return Ok(await _todoRepository.UpdateAsync(id, todoItem));
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create(TodoItem todoItem)
        {
            var createdItem = await _todoRepository.CreateAsync(todoItem);

            return CreatedAtAction(nameof(GetById), new { id = createdItem.Item1 }, createdItem.Item2);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!_todoRepository.Exists(id))
                return NoContent();

            await _todoRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
