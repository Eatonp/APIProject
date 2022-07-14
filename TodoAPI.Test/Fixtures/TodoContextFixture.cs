using Microsoft.EntityFrameworkCore;
using System;
using TodoAPI.Data.Database;

namespace TodoAPI.Test.Fixtures
{
    public class TodoContextFixture
    {
        public string DbName { get; set; }
        public TodoDbContext Context { get; set; }

        public TodoContextFixture()
        {
            DbName = $"TodoAPITests_{Guid.NewGuid()}";

            Context = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>().UseInMemoryDatabase(DbName).Options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}
