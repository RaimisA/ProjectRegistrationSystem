using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    public class AddressMapper : IAddressMapper
    {
        public Address Map(AddressRequestDto dto)
        {
            return new Address
            {
                City = dto.City,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber,
                ApartmentNumber = dto.ApartmentNumber
            };
        }

        public AddressResultDto Map(Address entity)
        {
            return new AddressResultDto
            {
                Id = entity.Id,
                City = entity.City,
                Street = entity.Street,
                HouseNumber = entity.HouseNumber,
                ApartmentNumber = entity.ApartmentNumber
            };
        }
    }
}