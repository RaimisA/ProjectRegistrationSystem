using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    /// <summary>
    /// Mapper for converting between User entities and DTOs.
    /// </summary>
    public class UserMapper : IUserMapper
    {
        /// <summary>
        /// Maps a UserRequestDto to a User entity.
        /// </summary>
        /// <param name="dto">The UserRequestDto.</param>
        /// <returns>The User entity.</returns>
        public User Map(UserRequestDto dto)
        {
            return new User
            {
                Username = dto.Username,
                // PasswordHash and Salt will be set in the service
            };
        }

        /// <summary>
        /// Maps a User entity to a UserResultDto.
        /// </summary>
        /// <param name="entity">The User entity.</param>
        /// <returns>The UserResultDto.</returns>
        public UserResultDto Map(User entity)
        {
            return new UserResultDto
            {
                Id = entity.Id,
                Username = entity.Username,
                Role = entity.Role
            };
        }
    }
}