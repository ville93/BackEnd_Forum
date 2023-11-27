using Microsoft.AspNetCore.Mvc;
using MyChat.Controllers;
using MyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Tests.ControllerTests
{
    public class ChannelControllerTests
    {
        [Fact]
        public void GetChannels_ShouldReturnAllChannels()
        {
            // Arrange
            var controller = new ChannelController();

            // Act
            var result = controller.GetChannels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var channels = Assert.IsType<List<Channel>>(okResult.Value);
            Assert.Equal(2, channels.Count);
        }
        [Fact]
        public void GetChannel_WithValidId_ShouldReturnChannel()
        {
            // Arrange
            var controller = new ChannelController();

            // Act
            var result = controller.GetChannel(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var channel = Assert.IsType<Channel>(okResult.Value);
            Assert.Equal(1, channel.Id);
        }

        [Fact]
        public void GetChannel_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new ChannelController();

            // Act
            var result = controller.GetChannel(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void AddChannel_ShouldCreateNewChannel()
        {
            // Arrange
            var controller = new ChannelController();
            var newChannel = new Channel { Name = "NewChannel" };

            // Act
            var result = controller.AddChannel(newChannel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdChannel = Assert.IsType<Channel>(createdAtActionResult.Value);
            Assert.Equal(3, createdChannel.Id); // Olettaen, että luotu ID on oikea
        }

        [Fact]
        public void UpdateChannel_WithValidId_ShouldUpdateChannel()
        {
            // Arrange
            var controller = new ChannelController();
            var updatedChannel = new Channel { Id = 1, Name = "UpdatedChannel" };

            // Act
            var result = controller.UpdateChannel(1, updatedChannel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var existingChannel = Assert.IsType<Channel>(okResult.Value);
            Assert.Equal("UpdatedChannel", existingChannel.Name);
        }

        [Fact]
        public void UpdateChannel_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new ChannelController();
            var updatedChannel = new Channel { Id = 999, Name = "UpdatedChannel" };

            // Act
            var result = controller.UpdateChannel(999, updatedChannel);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteChannel_WithValidId_ShouldDeleteChannel()
        {
            // Arrange
            var controller = new ChannelController();

            // Act
            var result = controller.DeleteChannel(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteChannel_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = new ChannelController();

            // Act
            var result = controller.DeleteChannel(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
