using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    /// <summary>
    /// Interface for mapping user entities and DTOs.
    /// </summary>
    public interface IUserMapper
    {
        /// <summary>
        /// Maps a User entity to a UserResultDto.
        /// </summary>
        /// <param name="entity">The User entity.</param>
        /// <returns>The UserResultDto.</returns>
        UserResultDto Map(User entity);

        /// <summary>
        /// Maps a UserRequestDto to a User entity.
        /// </summary>
        /// <param name="dto">The UserRequestDto.</param>
        /// <returns>The User entity.</returns>
        User Map(UserRequestDto dto);
    }
}