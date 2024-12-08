using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    public interface IAddressMapper
    {
        AddressResultDto Map(Address entity);
        Address Map(AddressRequestDto dto);
    }
}