using AutoMapper;
using TodoAPI.Data.Database;

namespace TodoAPI.Data.Repositories
{
    public interface ITodoRepository
    {
        TodoApi.Models.TodoItem? GetByID(Guid id);
        IEnumerable<TodoApi.Models.TodoItem> GetAll();
        Task<TodoApi.Models.TodoItem> UpdateAsync(Guid id, TodoApi.Models.TodoItem todoItem);
        Task<Tuple<Guid, TodoApi.Models.TodoItem>> CreateAsync(TodoApi.Models.TodoItem todoItem);
        Task DeleteAsync(Guid id);
        bool Exists(Guid id);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(TodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public TodoApi.Models.TodoItem? GetByID(Guid id)
        {
            var result = _context.TodoItems.Where(x => x.Id == id).FirstOrDefault();
            if (result == null)
                return null;

            return _mapper.Map<TodoApi.Models.TodoItem>(result);
        }

        public async Task<TodoApi.Models.TodoItem> UpdateAsync(Guid id, TodoApi.Models.TodoItem todoItem)
        {
            var dbModel = _context.TodoItems.Single(x => x.Id == id);

            dbModel.Name = todoItem.Name;
            dbModel.IsComplete = todoItem.IsComplete;
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoApi.Models.TodoItem>(dbModel);
        }

        public bool Exists(Guid id)
        {
            return _context.TodoItems.Any(x => x.Id == id);
        }

        public IEnumerable<TodoApi.Models.TodoItem> GetAll()
        {
            return _context.TodoItems.Select(x => _mapper.Map<TodoApi.Models.TodoItem>(x));
        }

        public async Task<Tuple<Guid, TodoApi.Models.TodoItem>> CreateAsync(TodoApi.Models.TodoItem todoItem)
        {
            var dbItem = _mapper.Map<TodoAPI.Data.Models.TodoItem>(todoItem);
            _context.Add(dbItem);
            await _context.SaveChangesAsync();

            return Tuple.Create(dbItem.Id, _mapper.Map<TodoApi.Models.TodoItem>(dbItem));
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = new TodoAPI.Data.Models.TodoItem() { Id = id };
            _context.Attach(item);
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
