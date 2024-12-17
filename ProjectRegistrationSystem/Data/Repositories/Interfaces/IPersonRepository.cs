using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

public interface IPersonRepository
{
    /// <summary>
    /// Gets a person by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the person.</param>
    /// <returns>The person entity.</returns>
    Task<Person> GetPersonByIdAsync(Guid id);

    /// <summary>
    /// Gets a person by their user ID.
    /// </summary>
    /// <param name="userId">The user ID associated with the person.</param>
    /// <returns>The person entity.</returns>
    Task<Person> GetPersonByUserIdAsync(Guid userId);

    /// <summary>
    /// Gets a person by their personal code.
    /// </summary>
    /// <param name="personalCode">The personal code of the person.</param>
    /// <returns>The person entity.</returns>
    Task<Person> GetPersonByPersonalCodeAsync(string personalCode);

    /// <summary>
    /// Gets a person by their phone number.
    /// </summary>
    /// <param name="phoneNumber">The phone number of the person.</param>
    /// <returns>The person entity.</returns>
    Task<Person> GetPersonByPhoneNumberAsync(string phoneNumber);

    /// <summary>
    /// Gets a person by their email.
    /// </summary>
    /// <param name="email">The email of the person.</param>
    /// <returns>The person entity.</returns>
    Task<Person> GetPersonByEmailAsync(string email);

    /// <summary>
    /// Adds a new person to the database.
    /// </summary>
    /// <param name="person">The person entity to add.</param>
    Task AddPersonAsync(Person person);

    /// <summary>
    /// Deletes a person from the database.
    /// </summary>
    /// <param name="person">The person entity to delete.</param>
    Task DeletePersonAsync(Person person);

    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    Task SaveChangesAsync();
}