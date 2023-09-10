namespace TodoApi.Data.Repositories
{
    public interface ITodoItemsRepository
    {
        Task<bool> ExistsAsync(long id);
        Task DeleteAsync(long id);
    }


    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly TodoContext _context;

        public TodoItemsRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            return todoItem != null;
        }

        public async Task DeleteAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
                return;

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}
