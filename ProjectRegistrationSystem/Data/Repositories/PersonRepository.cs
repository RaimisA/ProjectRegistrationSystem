using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetPersonByIdAsync(Guid id)
        {
            return await _context.Persons.Include(p => p.Address).Include(p => p.ProfilePicture).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person> GetPersonByUserIdAsync(Guid userId)
        {
            return await _context.Persons.Include(p => p.Address).Include(p => p.ProfilePicture).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddPersonAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}