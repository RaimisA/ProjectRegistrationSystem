using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing address entities.
    /// </summary>
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets an address by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the address.</param>
        /// <returns>The address entity.</returns>
        Task<Address> GetAddressByIdAsync(Guid id);

        /// <summary>
        /// Adds a new address to the database.
        /// </summary>
        /// <param name="address">The address entity to add.</param>
        Task AddAddressAsync(Address address);

        /// <summary>
        /// Deletes an address from the database.
        /// </summary>
        /// <param name="address">The address entity to delete.</param>
        Task DeleteAddressAsync(Address address);

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        Task SaveChangesAsync();
    }
}