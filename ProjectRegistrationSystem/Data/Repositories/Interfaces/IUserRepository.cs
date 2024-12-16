using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task SaveChangesAsync();
        Task UpdateUserRoleAsync(Guid userId, string role);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}