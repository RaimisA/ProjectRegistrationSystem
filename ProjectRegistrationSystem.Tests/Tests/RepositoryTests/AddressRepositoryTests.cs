using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.RepositoryTests
{
    public class AddressRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly AddressRepository _addressRepository;

        public AddressRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _addressRepository = new AddressRepository(_context);
        }

        [Theory, AddressData]
        public async Task GetAddressByIdAsync_ValidId_ReturnsAddress(Address address)
        {
            // Arrange
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            // Act
            var result = await _addressRepository.GetAddressByIdAsync(address.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
        }

        [Theory, AddressData]
        public async Task AddAddressAsync_ValidAddress_AddsAddress(Address address)
        {
            // Act
            await _addressRepository.AddAddressAsync(address);
            await _addressRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Addresses.FindAsync(address.Id);
            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
        }

        [Theory, AddressData]
        public async Task DeleteAddressAsync_ValidAddress_DeletesAddress(Address address)
        {
            // Arrange
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            // Act
            await _addressRepository.DeleteAddressAsync(address);

            // Assert
            var result = await _context.Addresses.FindAsync(address.Id);
            Assert.Null(result);
        }

        [Theory, AddressData]
        public async Task SaveChangesAsync_SavesChanges(Address address)
        {
            // Arrange
            await _context.Addresses.AddAsync(address);

            // Act
            await _addressRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Addresses.FindAsync(address.Id);
            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
        }
    }
}