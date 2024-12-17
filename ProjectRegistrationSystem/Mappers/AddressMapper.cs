using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    /// <summary>
    /// Mapper for converting between Address entities and DTOs.
    /// </summary>
    public class AddressMapper : IAddressMapper
    {
        /// <summary>
        /// Maps an AddressRequestDto to an Address entity.
        /// </summary>
        /// <param name="dto">The AddressRequestDto.</param>
        /// <returns>The Address entity.</returns>
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

        /// <summary>
        /// Maps an Address entity to an AddressResultDto.
        /// </summary>
        /// <param name="entity">The Address entity.</param>
        /// <returns>The AddressResultDto.</returns>
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