using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressByIdAsync(Guid id);
        Task AddAddressAsync(Address address);
        Task DeleteAddressAsync(Address address);
        Task SaveChangesAsync();
    }
}