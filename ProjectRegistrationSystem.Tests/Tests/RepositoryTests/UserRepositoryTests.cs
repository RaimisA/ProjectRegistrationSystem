using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.RepositoryTests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _userRepository = new UserRepository(_context);
        }

        private User CreateUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                PasswordHash = "passwordHash",
                PasswordSalt = "passwordSalt",
                Role = "User",
                Person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    PersonalCode = "12345678901",
                    PhoneNumber = "+1234567890",
                    Email = "john.doe@example.com",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        City = "Test City",
                        Street = "Test Street",
                        HouseNumber = "123",
                        ApartmentNumber = "456"
                    },
                    ProfilePicture = new Picture
                    {
                        Id = Guid.NewGuid(),
                        FileName = "profile.jpg",
                        Data = new byte[] { 0x20, 0x20, 0x20 },
                        ContentType = "image/jpeg",
                        Width = 100,
                        Height = 100
                    }
                }
            };
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsUser()
        {
            // Arrange
            var user = CreateUser();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserByIdAsync(user.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task GetUserByUsernameAsync_ValidUsername_ReturnsUser()
        {
            // Arrange
            var user = CreateUser();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetUserByUsernameAsync(user.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public async Task AddUserAsync_ValidUser_AddsUser()
        {
            // Arrange
            var user = CreateUser();

            // Act
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public async Task DeleteUserAsync_ValidUser_DeletesUser()
        {
            // Arrange
            var user = CreateUser();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            await _userRepository.DeleteUserAsync(user);

            // Assert
            var result = await _context.Users.FindAsync(user.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserRoleAsync_ValidUser_UpdatesRole()
        {
            // Arrange
            var user = CreateUser();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var newRole = "Admin";

            // Act
            await _userRepository.UpdateUserRoleAsync(user.Id, newRole);

            // Assert
            var result = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal(newRole, result.Role);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                CreateUser(),
                CreateUser(),
                CreateUser(),
                CreateUser(),
                CreateUser()
            };
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task SaveChangesAsync_SavesChanges()
        {
            // Arrange
            var user = CreateUser();
            await _context.Users.AddAsync(user);

            // Act
            await _userRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}