using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string username, string password);
        Task<bool> AddPersonInfoAsync(Guid userId, Person person);
        Task<Person> GetPersonInfoAsync(Guid id);
        Task<Person> GetPersonInfoByUserIdAsync(Guid userId);
        Task<bool> UpdateFirstNameAsync(Guid id, string firstName);
        Task<bool> UpdateLastNameAsync(Guid id, string lastName);
        Task<bool> UpdatePersonalCodeAsync(Guid id, string personalCode);
        Task<bool> UpdatePhoneNumberAsync(Guid id, string phoneNumber);
        Task<bool> UpdateEmailAsync(Guid id, string email);
        Task<bool> UpdateAddressAsync(Guid id, Address address);
        Task<bool> UpdateProfilePictureAsync(Guid id, Picture picture);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<(bool Success, string Role)> LoginAsync(string username, string password);
        Task<bool> UpdateUserRoleAsync(Guid userId, string role);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}