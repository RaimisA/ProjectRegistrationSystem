using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services.Interfaces
{
    /// <summary>
    /// Interface for managing user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The registered user entity.</returns>
        Task<User> RegisterUserAsync(string username, string password);

        /// <summary>
        /// Adds person information to a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="person">The person entity.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> AddPersonInfoAsync(Guid userId, Person person);

        /// <summary>
        /// Gets person information by person ID.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The person entity.</returns>
        Task<Person> GetPersonInfoAsync(Guid id);

        /// <summary>
        /// Gets person information by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The person entity.</returns>
        Task<Person> GetPersonInfoByUserIdAsync(Guid userId);

        /// <summary>
        /// Updates the first name of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="firstName">The new first name.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateFirstNameAsync(Guid id, string firstName);

        /// <summary>
        /// Updates the last name of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="lastName">The new last name.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateLastNameAsync(Guid id, string lastName);

        /// <summary>
        /// Updates the personal code of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="personalCode">The new personal code.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdatePersonalCodeAsync(Guid id, string personalCode);

        /// <summary>
        /// Updates the phone number of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdatePhoneNumberAsync(Guid id, string phoneNumber);

        /// <summary>
        /// Updates the email of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="email">The new email.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateEmailAsync(Guid id, string email);

        /// <summary>
        /// Updates the address of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="address">The new address.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateAddressAsync(Guid id, Address address);

        /// <summary>
        /// Updates the profile picture of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="picture">The new profile picture.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateProfilePictureAsync(Guid id, Picture picture);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> DeleteUserAsync(Guid id);

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user entity.</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A tuple containing a success flag and the user's role.</returns>
        Task<(bool Success, string Role)> LoginAsync(string username, string password);

        /// <summary>
        /// Updates the role of a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The new role.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        Task<bool> UpdateUserRoleAsync(Guid userId, string role);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of user entities.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Checks if person information is unique.
        /// </summary>
        /// <param name="personalCode">The personal code of the person.</param>
        /// <param name="phoneNumber">The phone number of the person.</param>
        /// <param name="email">The email of the person.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CheckPersonInfoAsync(string personalCode, string phoneNumber, string email);
    }
}