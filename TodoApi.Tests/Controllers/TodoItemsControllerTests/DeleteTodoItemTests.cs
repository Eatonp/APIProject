using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Data.Repositories;

namespace TodoApi.Tests.Controllers.TodoItemsControllerTests
{
    public class TodoItemsControllerTests
    {
        private Mock<ITodoItemsRepository> mockTodoItemsRepository;
        private TodoItemsController controller;

        public TodoItemsControllerTests()
        {
            mockTodoItemsRepository = new Mock<ITodoItemsRepository>();
            controller = new TodoItemsController(mockTodoItemsRepository.Object);
        }

        [Fact]
        public async Task WhenDeletingATodoItemThatDoesNotExist_NotFoundResultIsReturned()
        {
            mockTodoItemsRepository
                .Setup(x => x.ExistsAsync(It.IsAny<long>()))
                .ReturnsAsync(false);

            var result = await controller.DeleteTodoItem(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task WhenDeletingATodoItemThatDoesExist_ItCallsTheRepositoryToDeleteTheItemAndNoContentIsReturned()
        {
            mockTodoItemsRepository
                .Setup(x => x.ExistsAsync(It.IsAny<long>()))
                .ReturnsAsync(true);

            var itemToDelete = 2;

            var result = await controller.DeleteTodoItem(itemToDelete);

            mockTodoItemsRepository.Verify(x => x.DeleteAsync(itemToDelete), Times.Once);

            Assert.IsType<NoContentResult>(result);
        }
    }
}