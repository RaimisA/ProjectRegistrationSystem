using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories
{
    /// <summary>
    /// Repository for managing person entities.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonByIdAsync(Guid id)
        {
            return await _context.Persons.Include(p => p.Address).Include(p => p.ProfilePicture).FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Gets a person by their user ID.
        /// </summary>
        /// <param name="userId">The user ID associated with the person.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonByUserIdAsync(Guid userId)
        {
            return await _context.Persons.Include(p => p.Address).Include(p => p.ProfilePicture).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Gets a person by their personal code.
        /// </summary>
        /// <param name="personalCode">The personal code of the person.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonByPersonalCodeAsync(string personalCode)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.PersonalCode == personalCode);
        }

        /// <summary>
        /// Gets a person by their phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number of the person.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// Gets a person by their email.
        /// </summary>
        /// <param name="email">The email of the person.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonByEmailAsync(string email)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
        }

        /// <summary>
        /// Adds a new person to the database.
        /// </summary>
        /// <param name="person">The person entity to add.</param>
        public async Task AddPersonAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        /// <summary>
        /// Deletes a person from the database.
        /// </summary>
        /// <param name="person">The person entity to delete.</param>
        public async Task DeletePersonAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}