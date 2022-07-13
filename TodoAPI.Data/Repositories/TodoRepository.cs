using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPI.Data.Database;
using TodoAPI.Data.Models;

namespace TodoAPI.Data.Repositories
{
    public interface ITodoRepository
    {
        TodoItem? GetByID(Guid id);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public TodoItem? GetByID(Guid id)
        {
            return _context.TodoItems.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
