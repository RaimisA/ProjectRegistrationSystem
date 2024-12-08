using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User Map(UserRequestDto dto)
        {
            return new User
            {
                Username = dto.Username,
                // PasswordHash and Salt will be set in the service
            };
        }

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