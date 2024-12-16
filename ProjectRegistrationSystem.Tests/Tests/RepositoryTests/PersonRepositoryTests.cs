using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.RepositoryTests
{
    public class PersonRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonRepository _personRepository;

        public PersonRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _personRepository = new PersonRepository(_context);
        }

        [Theory, PersonData]
        public async Task GetPersonByIdAsync_ValidId_ReturnsPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            var result = await _personRepository.GetPersonByIdAsync(person.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
        }

        [Theory, PersonData]
        public async Task GetPersonByUserIdAsync_ValidUserId_ReturnsPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            var result = await _personRepository.GetPersonByUserIdAsync(person.UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.UserId, result.UserId);
        }

        [Theory, PersonData]
        public async Task GetPersonByPersonalCodeAsync_ValidPersonalCode_ReturnsPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            var result = await _personRepository.GetPersonByPersonalCodeAsync(person.PersonalCode);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.PersonalCode, result.PersonalCode);
        }

        [Theory, PersonData]
        public async Task GetPersonByPhoneNumberAsync_ValidPhoneNumber_ReturnsPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            var result = await _personRepository.GetPersonByPhoneNumberAsync(person.PhoneNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.PhoneNumber, result.PhoneNumber);
        }

        [Theory, PersonData]
        public async Task GetPersonByEmailAsync_ValidEmail_ReturnsPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            var result = await _personRepository.GetPersonByEmailAsync(person.Email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(person.Email, result.Email);
        }

        [Theory, PersonData]
        public async Task AddPersonAsync_ValidPerson_AddsPerson(Person person)
        {
            // Act
            await _personRepository.AddPersonAsync(person);
            await _personRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Persons.FindAsync(person.Id);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
        }

        [Theory, PersonData]
        public async Task DeletePersonAsync_ValidPerson_DeletesPerson(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            // Act
            await _personRepository.DeletePersonAsync(person);

            // Assert
            var result = await _context.Persons.FindAsync(person.Id);
            Assert.Null(result);
        }

        [Theory, PersonData]
        public async Task SaveChangesAsync_SavesChanges(Person person)
        {
            // Arrange
            await _context.Persons.AddAsync(person);

            // Act
            await _personRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Persons.FindAsync(person.Id);
            Assert.NotNull(result);
            Assert.Equal(person.Id, result.Id);
        }
    }
}