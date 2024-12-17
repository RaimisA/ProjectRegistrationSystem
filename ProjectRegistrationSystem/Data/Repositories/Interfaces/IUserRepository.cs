using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing user entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user entity.</returns>
        Task<User> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Gets a user by their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The user entity.</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        Task AddUserAsync(User user);

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="user">The user entity to delete.</param>
        Task DeleteUserAsync(User user);

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Updates the role of an existing user in the database.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="role">The new role of the user.</param>
        Task UpdateUserRoleAsync(Guid userId, string role);

        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A list of user entities.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}