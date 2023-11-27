using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyChat.Controllers;
using MyChat.Models.DTO;
using Xunit;
using Moq;
using MyChat.Mocks;
using MyChat.Tests.Mocks;

namespace MyChat.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_WithValidModel_ShouldReturnOk()
        {
            // Arrange
            var userManager = UserManagerMock.GetMockUserManager<IdentityUser>();
            var signInManager = SignInManagerMock.GetMockSignInManager<IdentityUser>();
            var configuration = ConfigurationMock.GetMockConfiguration();

            var controller = new AuthController(userManager.Object, signInManager.Object, configuration.Object);
            var model = new RegisterRequestDTO { Email = "test@example.com", Password = "TestPassword123" };

            // Act
            var result = await controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal("Registration successful", response["Message"]);
        }

        [Fact]
        public async Task Register_WithInvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var userManager = UserManagerMock.GetMockUserManager<IdentityUser>();
            var signInManager = SignInManagerMock.GetMockSignInManager<IdentityUser>();
            var configuration = ConfigurationMock.GetMockConfiguration();

            var controller = new AuthController(userManager.Object, signInManager.Object, configuration.Object);
            var model = new RegisterRequestDTO(); // Invalid model

            // Act
            var result = await controller.Register(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<Dictionary<string, object>>(badRequestResult.Value);
            Assert.Equal("Registration failed", response["Message"]);
            Assert.NotNull(response["Errors"]);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnOkWithToken()
        {
            // Arrange
            var userManager = UserManagerMock.GetMockUserManager<IdentityUser>();
            var signInManager = SignInManagerMock.GetMockSignInManager<IdentityUser>();
            var configuration = ConfigurationMock.GetMockConfiguration();

            var controller = new AuthController(userManager.Object, signInManager.Object, configuration.Object);
            var model = new LoginRequestDTO { Email = "test@example.com", Password = "TestPassword123" };

            // Act
            var result = await controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponseDTO>(okResult.Value);
            Assert.NotNull(response.Token);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
        {
            // Arrange
            var userManager = UserManagerMock.GetMockUserManager<IdentityUser>();
            var signInManager = SignInManagerMock.GetMockSignInManager<IdentityUser>();
            var configuration = ConfigurationMock.GetMockConfiguration();

            var controller = new AuthController(userManager.Object, signInManager.Object, configuration.Object);
            var model = new LoginRequestDTO { Email = "invalid@example.com", Password = "InvalidPassword" };

            // Act
            var result = await controller.Login(model);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var response = Assert.IsType<Dictionary<string, string>>(unauthorizedResult.Value);
            Assert.Equal("Invalid credentials", response["Message"]);
        }
    }
}
