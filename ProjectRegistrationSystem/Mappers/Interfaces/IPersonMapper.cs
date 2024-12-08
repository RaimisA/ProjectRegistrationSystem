using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    public interface IPersonMapper
    {
        PersonResultDto Map(Person entity);
        Person Map(PersonRequestDto dto);
    }
}