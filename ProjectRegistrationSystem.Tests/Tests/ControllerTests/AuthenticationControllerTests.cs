using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectRegistrationSystem.Controllers;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Services.Interfaces;
using ProjectRegistrationSystem.Tests.DataAttributes;
using ProjectRegistrationSystem.Tests.Dtos;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.ControllerTests
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<ILogger<AuthenticationController>> _mockLogger;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockJwtService = new Mock<IJwtService>();
            _mockLogger = new Mock<ILogger<AuthenticationController>>();
            _controller = new AuthenticationController(_mockUserService.Object, _mockJwtService.Object, _mockLogger.Object);
        }

        [Theory]
        [UserRequestDtoData]
        public async Task Register_ValidUser_ReturnsOk(UserRequestDto userRequestDto)
        {
            // Arrange
            _mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User { Username = userRequestDto.Username });

            // Act
            var result = await _controller.Register(userRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.Contains("User registered successfully", okResult.Value.ToString());
        }

        [Theory]
        [UserRequestDtoData]
        public async Task Register_InvalidUser_ReturnsBadRequest(UserRequestDto userRequestDto)
        {
            // Arrange
            _mockUserService.Setup(s => s.RegisterUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException("User already exists"));

            // Act
            var result = await _controller.Register(userRequestDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            Assert.Contains("User already exists", badRequestResult.Value.ToString());
        }

        [Theory]
        [UserLoginRequestDtoData]
        public async Task Login_ValidUser_ReturnsOk(UserLoginRequestDto userLoginRequestDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserService.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((true, "User"));
            _mockUserService.Setup(s => s.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId, Username = userLoginRequestDto.Username });
            _mockJwtService.Setup(s => s.GetJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns("fake-jwt-token");

            // Act
            var result = await _controller.Login(userLoginRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = okResult.Value;
            Assert.Equal("fake-jwt-token", resultValue.GetType().GetProperty("Token").GetValue(resultValue, null));
            Assert.Equal(userId, resultValue.GetType().GetProperty("UserId").GetValue(resultValue, null));
        }

        [Theory]
        [UserLoginRequestDtoData]
        public async Task Login_InvalidUser_ReturnsUnauthorized(UserLoginRequestDto userLoginRequestDto)
        {
            // Arrange
            _mockUserService.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((false, (string?)null));

            // Act
            var result = await _controller.Login(userLoginRequestDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);
        }

        [Theory]
        [UserLoginRequestDtoData]
        public async Task Login_UserNotFound_ReturnsNotFound(UserLoginRequestDto userLoginRequestDto)
        {
            // Arrange
            _mockUserService.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((true, "User"));
            _mockUserService.Setup(s => s.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.Login(userLoginRequestDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found.", notFoundResult.Value);
        }
    }
}