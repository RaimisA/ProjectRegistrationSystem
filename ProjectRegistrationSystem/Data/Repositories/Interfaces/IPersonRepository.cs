using ProjectRegistrationSystem.Data.Entities;

public interface IPersonRepository
{
    Task<Person> GetPersonByIdAsync(Guid id);
    Task<Person> GetPersonByUserIdAsync(Guid userId);
    Task<Person> GetPersonByPersonalCodeAsync(string personalCode);
    Task<Person> GetPersonByPhoneNumberAsync(string phoneNumber);
    Task<Person> GetPersonByEmailAsync(string email);
    Task AddPersonAsync(Person person);
    Task DeletePersonAsync(Person person);
    Task SaveChangesAsync();
}