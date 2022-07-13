using Microsoft.EntityFrameworkCore;
using TodoAPI.Data.Models;

namespace TodoAPI.Data.Database
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options): base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<TodoItem>()
                .HasData(
                    new TodoItem() { Id = Guid.NewGuid(), IsComplete = false, Name = "Jerry", Secret = "Guy" },
                    new TodoItem() { Id = Guid.NewGuid(), IsComplete = true, Name = "Sam", Secret = "Test" },
                    new TodoItem() { Id = Guid.NewGuid(), IsComplete = false, Name = "Ian", Secret = "Other" }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}