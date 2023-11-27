using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyChat.Controllers;
using MyChat.Models;
using Xunit;

namespace MyChat.Tests.Controllers
{
    public class MessageControllerTests
    {
        [Fact]
        public void GetMessages_ShouldReturnEmptyList()
        {
            // Arrange
            var controller = new MessageController();

            // Act
            var result = controller.GetMessages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var messages = Assert.IsType<List<Message>>(okResult.Value);
            Assert.Empty(messages);
        }

        [Fact]
        public void GetMessage_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new MessageController();

            // Act
            var result = controller.GetMessage(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void AddMessage_ShouldReturnCreatedWithNewMessage()
        {
            // Arrange
            var controller = new MessageController();
            var newMessage = new Message { Content = "Hello, World!" };

            // Act
            var result = controller.AddMessage(newMessage);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var message = Assert.IsType<Message>(createdAtActionResult.Value);
            Assert.Equal(newMessage.Content, message.Content);
            Assert.Equal(1, message.Id); // Assuming it's the first message
        }

        [Fact]
        public void DeleteMessage_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new MessageController();

            // Act
            var result = controller.DeleteMessage(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteMessage_ShouldRemoveMessage()
        {
            // Arrange
            var controller = new MessageController();
            var newMessage = new Message { Content = "Hello, World!" };
            controller.AddMessage(newMessage); // Adding a message to delete

            // Act
            var result = controller.DeleteMessage(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Empty(controller.GetMessages().Value);
        }
    }
}
