using Microsoft.AspNetCore.Mvc;
using MyChat.Controllers;
using MyChat.Models;
using MyChat.Services;
using System.Collections.Generic;
using Xunit;

namespace MyChat.Tests.ControllerTests
{
    public class ChannelControllerTests
    {
        [Fact]
        public void GetChannels_ShouldReturnAllChannels()
        {
            // Arrange
            var channelService = new ChannelService(); 
            var controller = new ChannelController(channelService);

            // Act
            var result = controller.GetChannels();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var channels = Assert.IsType<List<Channel>>(okResult.Value);
            Assert.Equal(2, channels.Count);
        }
    }
}
