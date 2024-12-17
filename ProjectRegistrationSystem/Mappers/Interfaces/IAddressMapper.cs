using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    /// <summary>
    /// Interface for mapping address entities and DTOs.
    /// </summary>
    public interface IAddressMapper
    {
        /// <summary>
        /// Maps an Address entity to an AddressResultDto.
        /// </summary>
        /// <param name="entity">The Address entity.</param>
        /// <returns>The AddressResultDto.</returns>
        AddressResultDto Map(Address entity);

        /// <summary>
        /// Maps an AddressRequestDto to an Address entity.
        /// </summary>
        /// <param name="dto">The AddressRequestDto.</param>
        /// <returns>The Address entity.</returns>
        Address Map(AddressRequestDto dto);
    }
}