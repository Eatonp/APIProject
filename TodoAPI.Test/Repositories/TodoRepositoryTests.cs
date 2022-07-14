using AutoMapper;
using System;
using TodoApi.Mappers;
using TodoAPI.Data.Models;
using TodoAPI.Data.Repositories;
using TodoAPI.Test.Fixtures;
using Xunit;

namespace TodoAPI.Test
{
    public class TodoRepositoryTests : IDisposable
    {
        private TodoContextFixture _todoContextFixture;
        private TodoRepository _todoRepository;

        public TodoRepositoryTests()
        {
            var mapper = new MapperConfiguration(x => x.AddProfile<MappingProfile>()).CreateMapper();
            _todoContextFixture = new TodoContextFixture();

            _todoRepository = new TodoRepository(_todoContextFixture.Context, mapper);
        }

        public void Dispose()
        {
            _todoContextFixture.Dispose();
        }

        [Fact]
        public void WhenGettingAnItemByIDThatExists_TheItemIsReturned()
        {
            var dbItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                IsComplete = true,
                Name = "Jerry",
                Secret = "test"
            };

            _todoContextFixture.Context.Add(dbItem);
            _todoContextFixture.Context.SaveChanges();

            var result = _todoRepository.GetByID(dbItem.Id);

            Assert.Equal(dbItem.Name, result?.Name);
            Assert.Equal(dbItem.IsComplete, result?.IsComplete);
        }

        [Fact]
        public void WhenGettingAnItemByIDThatDoesNotExists_NullIsReturned()
        {
            var result = _todoRepository.GetByID(Guid.NewGuid());
            Assert.Null(result);
        }
    }
}