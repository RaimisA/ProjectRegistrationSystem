using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectRegistrationSystem.Controllers;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Mappers.Interfaces;
using ProjectRegistrationSystem.Services.Interfaces;
using ProjectRegistrationSystem.Tests.DataAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IPersonMapper> _mockPersonMapper;
        private readonly Mock<IAddressMapper> _mockAddressMapper;
        private readonly Mock<IPictureService> _mockPictureService;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockPersonMapper = new Mock<IPersonMapper>();
            _mockAddressMapper = new Mock<IAddressMapper>();
            _mockPictureService = new Mock<IPictureService>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _controller = new UserController(_mockUserService.Object, _mockPersonMapper.Object, _mockAddressMapper.Object, _mockPictureService.Object, _mockLogger.Object);
        }

        [Theory]
        [PersonRequestDtoData]
        public async Task AddPersonInfo_ValidData_ReturnsOk(PersonRequestDto personRequestDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var profilePicture = new Mock<IFormFile>();
            var memoryStream = new MemoryStream();
            profilePicture.Setup(p => p.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);
            profilePicture.Setup(p => p.FileName).Returns("profile.jpg");
            profilePicture.Setup(p => p.ContentType).Returns("image/jpeg");

            _mockUserService.Setup(s => s.CheckPersonInfoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockPersonMapper.Setup(m => m.Map(It.IsAny<PersonRequestDto>())).Returns(new Person());
            _mockPictureService.Setup(p => p.ProcessAndSavePictureAsync(It.IsAny<PictureRequestDto>(), null)).ReturnsAsync(new Picture());
            _mockUserService.Setup(s => s.AddPersonInfoAsync(It.IsAny<Guid>(), It.IsAny<Person>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddPersonInfo(userId, personRequestDto, profilePicture.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Person information added successfully.", okResult.Value);
        }

        [Theory]
        [PersonRequestDtoData]
        public async Task AddPersonInfo_InvalidData_ReturnsBadRequest(PersonRequestDto personRequestDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var profilePicture = new Mock<IFormFile>();

            _mockUserService.Setup(s => s.CheckPersonInfoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new InvalidOperationException("Validation failed"));

            // Act
            var result = await _controller.AddPersonInfo(userId, personRequestDto, profilePicture.Object);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Validation failed", badRequestResult.Value);
        }

        [Fact]
        public async Task GetPersonInfo_ValidUserId_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
            var personResultDto = new PersonResultDto { Id = person.Id, FirstName = "John", LastName = "Doe" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockPersonMapper.Setup(m => m.Map(It.IsAny<Person>())).Returns(personResultDto);

            // Act
            var result = await _controller.GetPersonInfo(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(personResultDto, okResult.Value);
        }

        [Fact]
        public async Task GetPersonInfo_InvalidUserId_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.GetPersonInfo(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [UpdateFirstNameDtoData]
        public async Task UpdateFirstName_ValidData_ReturnsOk(UpdateFirstNameDto updateFirstNameDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), FirstName = "Jane" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockUserService.Setup(s => s.UpdateFirstNameAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateFirstName(userId, updateFirstNameDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("First name updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdateFirstNameDtoData]
        public async Task UpdateFirstName_InvalidUserId_ReturnsNotFound(UpdateFirstNameDto updateFirstNameDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdateFirstName(userId, updateFirstNameDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [UpdateLastNameDtoData]
        public async Task UpdateLastName_ValidData_ReturnsOk(UpdateLastNameDto updateLastNameDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), LastName = "Smith" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockUserService.Setup(s => s.UpdateLastNameAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateLastName(userId, updateLastNameDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Last name updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdateLastNameDtoData]
        public async Task UpdateLastName_InvalidUserId_ReturnsNotFound(UpdateLastNameDto updateLastNameDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdateLastName(userId, updateLastNameDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [UpdatePersonalCodeDtoData]
        public async Task UpdatePersonalCode_ValidData_ReturnsOk(UpdatePersonalCodeDto updatePersonalCodeDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), PersonalCode = "09876543210" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockUserService.Setup(s => s.CheckPersonInfoAsync(It.IsAny<string>(), null, null)).Returns(Task.CompletedTask);
            _mockUserService.Setup(s => s.UpdatePersonalCodeAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdatePersonalCode(userId, updatePersonalCodeDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Personal code updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdatePersonalCodeDtoData]
        public async Task UpdatePersonalCode_InvalidUserId_ReturnsNotFound(UpdatePersonalCodeDto updatePersonalCodeDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdatePersonalCode(userId, updatePersonalCodeDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [UpdatePhoneNumberDtoData]
        public async Task UpdatePhoneNumber_ValidData_ReturnsOk(UpdatePhoneNumberDto updatePhoneNumberDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), PhoneNumber = "+0987654321" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockUserService.Setup(s => s.CheckPersonInfoAsync(null, It.IsAny<string>(), null)).Returns(Task.CompletedTask);
            _mockUserService.Setup(s => s.UpdatePhoneNumberAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdatePhoneNumber(userId, updatePhoneNumberDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Phone number updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdatePhoneNumberDtoData]
        public async Task UpdatePhoneNumber_InvalidUserId_ReturnsNotFound(UpdatePhoneNumberDto updatePhoneNumberDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdatePhoneNumber(userId, updatePhoneNumberDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [UpdateEmailDtoData]
        public async Task UpdateEmail_ValidData_ReturnsOk(UpdateEmailDto updateEmailDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), Email = "jane.doe@example.com" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockUserService.Setup(s => s.CheckPersonInfoAsync(null, null, It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockUserService.Setup(s => s.UpdateEmailAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateEmail(userId, updateEmailDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Email updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdateEmailDtoData]
        public async Task UpdateEmail_InvalidUserId_ReturnsNotFound(UpdateEmailDto updateEmailDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdateEmail(userId, updateEmailDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Theory]
        [AddressRequestDtoData]
        public async Task UpdateAddress_ValidData_ReturnsOk(AddressRequestDto addressRequestDto)
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = new Person { Id = Guid.NewGuid(), Address = new Address() };
            var address = new Address { Id = Guid.NewGuid(), City = "City", Street = "Street", HouseNumber = "123", ApartmentNumber = "1" };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockAddressMapper.Setup(m => m.Map(It.IsAny<AddressRequestDto>())).Returns(address);
            _mockUserService.Setup(s => s.UpdateAddressAsync(It.IsAny<Guid>(), It.IsAny<Address>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateAddress(userId, addressRequestDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Address updated successfully.", okResult.Value);
        }

        [Theory]
        [AddressRequestDtoData]
        public async Task UpdateAddress_InvalidUserId_ReturnsNotFound(AddressRequestDto addressRequestDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdateAddress(userId, addressRequestDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Fact]
        public async Task UpdateProfilePicture_ValidData_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var profilePicture = new Mock<IFormFile>();
            var memoryStream = new MemoryStream();
            profilePicture.Setup(p => p.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);
            profilePicture.Setup(p => p.FileName).Returns("profile.jpg");
            profilePicture.Setup(p => p.ContentType).Returns("image/jpeg");
            var person = new Person { Id = Guid.NewGuid(), ProfilePicture = new Picture() };

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(person);
            _mockPictureService.Setup(p => p.ProcessAndSavePictureAsync(It.IsAny<PictureRequestDto>(), It.IsAny<Guid?>())).ReturnsAsync(new Picture());
            _mockUserService.Setup(s => s.UpdateProfilePictureAsync(It.IsAny<Guid>(), It.IsAny<Picture>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateProfilePicture(userId, profilePicture.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Profile picture updated successfully.", okResult.Value);
        }

        [Fact]
        public async Task UpdateProfilePicture_InvalidUserId_ReturnsNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var profilePicture = new Mock<IFormFile>();

            _mockUserService.Setup(s => s.GetPersonInfoByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person)null);

            // Act
            var result = await _controller.UpdateProfilePicture(userId, profilePicture.Object);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var resultValue = notFoundResult.Value.GetType().GetProperty("Message").GetValue(notFoundResult.Value, null);
            Assert.Equal($"Person not found for user ID: {userId}", resultValue);
        }

        [Fact]
        public async Task DeleteUser_ValidUserId_ReturnsOk()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.DeleteUserAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User deleted successfully.", okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_InvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.DeleteUserAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to delete user.", badRequestResult.Value);
        }

        [Theory]
        [UpdateUserRoleDtoData]
        public async Task UpdateRole_ValidData_ReturnsOk(UpdateUserRoleDto updateUserRoleDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.UpdateUserRoleAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateRole(userId, updateUserRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Role updated successfully.", okResult.Value);
        }

        [Theory]
        [UpdateUserRoleDtoData]
        public async Task UpdateRole_InvalidUserId_ReturnsBadRequest(UpdateUserRoleDto updateUserRoleDto)
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.UpdateUserRoleAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateRole(userId, updateUserRoleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to update role.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOk()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = Guid.NewGuid(), Username = "user1", Role = "User" },
        new User { Id = Guid.NewGuid(), Username = "user2", Role = "Admin" }
    };

            _mockUserService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = okResult.Value;
            var userList = Assert.IsAssignableFrom<IEnumerable<object>>(resultValue);
            Assert.Equal(2, userList.Count());
        }
    }
}