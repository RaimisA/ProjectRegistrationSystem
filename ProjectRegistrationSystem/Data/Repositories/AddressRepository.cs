using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories
{
    /// <summary>
    /// Repository for managing address entities.
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets an address by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the address.</param>
        /// <returns>The address entity.</returns>
        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Adds a new address to the database.
        /// </summary>
        /// <param name="address">The address entity to add.</param>
        public async Task AddAddressAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        /// <summary>
        /// Deletes an address from the database.
        /// </summary>
        /// <param name="address">The address entity to delete.</param>
        public async Task DeleteAddressAsync(Address address)
        {
            _context.Addresses.Remove(address);
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