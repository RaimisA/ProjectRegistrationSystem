using AutoFixture;
using Moq;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using ProjectRegistrationSystem.Services;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly Mock<IAddressRepository> _mockAddressRepository;
        private readonly Mock<IPictureRepository> _mockPictureRepository;
        private readonly UserService _userService;
        private readonly IFixture _fixture;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockPictureRepository = new Mock<IPictureRepository>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockPersonRepository.Object,
                _mockAddressRepository.Object,
                _mockPictureRepository.Object
            );

            _fixture = new Fixture();
            _fixture.Customizations.Add(new UserSpecimenBuilder());
            _fixture.Customizations.Add(new PersonSpecimenBuilder());
            _fixture.Customizations.Add(new AddressSpecimenBuilder());
            _fixture.Customizations.Add(new PictureSpecimenBuilder());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task RegisterUserAsync_ValidData_ReturnsUser()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            _mockUserRepository.Setup(r => r.GetUserByUsernameAsync(username)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.RegisterUserAsync(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            _mockUserRepository.Verify(r => r.AddUserAsync(It.IsAny<User>()), Times.Once);
            _mockUserRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_UserAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var existingUser = _fixture.Build<User>()
                                       .Without(u => u.Person)
                                       .Create();
            _mockUserRepository.Setup(r => r.GetUserByUsernameAsync(username)).ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.RegisterUserAsync(username, password));
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var user = _userService.CreateUser(username, password);
            _mockUserRepository.Setup(r => r.GetUserByUsernameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _userService.LoginAsync(username, password);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(user.Role, result.Role);
        }

        [Fact]
        public async Task LoginAsync_InvalidCredentials_ReturnsFailure()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            _mockUserRepository.Setup(r => r.GetUserByUsernameAsync(username)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.LoginAsync(username, password);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Role);
        }

        [Fact]
        public async Task AddPersonInfoAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            var user = _fixture.Build<User>()
                               .Without(u => u.Person)
                               .Create();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.AddPersonInfoAsync(userId, person);

            // Assert
            Assert.True(result);
            _mockPersonRepository.Verify(r => r.AddPersonAsync(person), Times.Once);
            _mockUserRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddPersonInfoAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.AddPersonInfoAsync(userId, person);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateFirstNameAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var firstName = "NewFirstName";
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdateFirstNameAsync(personId, firstName);

            // Assert
            Assert.True(result);
            Assert.Equal(firstName, person.FirstName);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateFirstNameAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var firstName = "NewFirstName";
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdateFirstNameAsync(personId, firstName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateLastNameAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var lastName = "NewLastName";
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdateLastNameAsync(personId, lastName);

            // Assert
            Assert.True(result);
            Assert.Equal(lastName, person.LastName);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateLastNameAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var lastName = "NewLastName";
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdateLastNameAsync(personId, lastName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePersonalCodeAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var personalCode = "12345678901";
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdatePersonalCodeAsync(personId, personalCode);

            // Assert
            Assert.True(result);
            Assert.Equal(personalCode, person.PersonalCode);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePersonalCodeAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var personalCode = "12345678901";
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdatePersonalCodeAsync(personId, personalCode);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdatePhoneNumberAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var phoneNumber = "+1234567890";
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdatePhoneNumberAsync(personId, phoneNumber);

            // Assert
            Assert.True(result);
            Assert.Equal(phoneNumber, person.PhoneNumber);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdatePhoneNumberAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var phoneNumber = "+1234567890";
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdatePhoneNumberAsync(personId, phoneNumber);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateEmailAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var email = "newemail@example.com";
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdateEmailAsync(personId, email);

            // Assert
            Assert.True(result);
            Assert.Equal(email, person.Email);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateEmailAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var email = "newemail@example.com";
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdateEmailAsync(personId, email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAddressAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var address = _fixture.Create<Address>();
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdateAddressAsync(personId, address);

            // Assert
            Assert.True(result);
            Assert.Equal(address.City, person.Address.City);
            Assert.Equal(address.Street, person.Address.Street);
            Assert.Equal(address.HouseNumber, person.Address.HouseNumber);
            Assert.Equal(address.ApartmentNumber, person.Address.ApartmentNumber);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAddressAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var address = _fixture.Create<Address>();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdateAddressAsync(personId, address);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateProfilePictureAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var picture = _fixture.Create<Picture>();
            var person = _fixture.Build<Person>()
                                 .Without(p => p.User)
                                 .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync(person);

            // Act
            var result = await _userService.UpdateProfilePictureAsync(personId, picture);

            // Assert
            Assert.True(result);
            _mockPictureRepository.Verify(r => r.UpdatePictureAsync(It.IsAny<Picture>()), Times.Once);
            _mockPersonRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProfilePictureAsync_PersonNotFound_ReturnsFalse()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var picture = _fixture.Create<Picture>();
            _mockPersonRepository.Setup(r => r.GetPersonByIdAsync(personId)).ReturnsAsync((Person)null);

            // Act
            var result = await _userService.UpdateProfilePictureAsync(personId, picture);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = _fixture.Build<User>()
                               .Without(u => u.Person)
                               .Create();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.True(result);
            _mockUserRepository.Verify(r => r.DeleteUserAsync(user), Times.Once);
            _mockUserRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ValidData_ReturnsUser()
        {
            // Arrange
            var username = "testuser";
            var user = _fixture.Build<User>()
                               .Without(u => u.Person)
                               .Create();
            _mockUserRepository.Setup(r => r.GetUserByUsernameAsync(username)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByUsernameAsync(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public async Task UpdateUserRoleAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var role = "admin";

            // Act
            var result = await _userService.UpdateUserRoleAsync(userId, role);

            // Assert
            Assert.True(result);
            _mockUserRepository.Verify(r => r.UpdateUserRoleAsync(userId, "Admin"), Times.Once);
        }


        [Fact]
        public async Task GetAllUsersAsync_ReturnsUsers()
        {
            // Arrange
            var users = _fixture.Build<User>()
                                .Without(u => u.Person)
                                .CreateMany(5);
            _mockUserRepository.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task CheckPersonInfoAsync_ValidData_DoesNotThrow()
        {
            // Arrange
            var personalCode = "12345678901";
            var phoneNumber = "+1234567890";
            var email = "test@example.com";
            _mockPersonRepository.Setup(r => r.GetPersonByPersonalCodeAsync(personalCode)).ReturnsAsync((Person)null);
            _mockPersonRepository.Setup(r => r.GetPersonByPhoneNumberAsync(phoneNumber)).ReturnsAsync((Person)null);
            _mockPersonRepository.Setup(r => r.GetPersonByEmailAsync(email)).ReturnsAsync((Person)null);

            // Act & Assert
            await _userService.CheckPersonInfoAsync(personalCode, phoneNumber, email);
        }

        [Fact]
        public async Task CheckPersonInfoAsync_PersonalCodeExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var personalCode = "12345678901";
            var phoneNumber = "+1234567890";
            var email = "test@example.com";
            var existingPerson = _fixture.Build<Person>()
                                         .Without(p => p.User)
                                         .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByPersonalCodeAsync(personalCode)).ReturnsAsync(existingPerson);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.CheckPersonInfoAsync(personalCode, phoneNumber, email));
        }

        [Fact]
        public async Task CheckPersonInfoAsync_PhoneNumberExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var personalCode = "12345678901";
            var phoneNumber = "+1234567890";
            var email = "test@example.com";
            var existingPerson = _fixture.Build<Person>()
                                         .Without(p => p.User)
                                         .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByPhoneNumberAsync(phoneNumber)).ReturnsAsync(existingPerson);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.CheckPersonInfoAsync(personalCode, phoneNumber, email));
        }

        [Fact]
        public async Task CheckPersonInfoAsync_EmailExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var personalCode = "12345678901";
            var phoneNumber = "+1234567890";
            var email = "test@example.com";
            var existingPerson = _fixture.Build<Person>()
                                         .Without(p => p.User)
                                         .Create();
            _mockPersonRepository.Setup(r => r.GetPersonByEmailAsync(email)).ReturnsAsync(existingPerson);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.CheckPersonInfoAsync(personalCode, phoneNumber, email));
        }
    }
}