using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    public interface IUserMapper
    {
        UserResultDto Map(User entity);
        User Map(UserRequestDto dto);
    }
}