using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonByIdAsync(Guid id);
        Task<Person> GetPersonByUserIdAsync(Guid userId);
        Task AddPersonAsync(Person person);
        Task DeletePersonAsync(Person person);
        Task SaveChangesAsync();
    }
}