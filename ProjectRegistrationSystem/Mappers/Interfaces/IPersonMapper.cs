using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    /// <summary>
    /// Interface for mapping person entities and DTOs.
    /// </summary>
    public interface IPersonMapper
    {
        /// <summary>
        /// Maps a Person entity to a PersonResultDto.
        /// </summary>
        /// <param name="entity">The Person entity.</param>
        /// <returns>The PersonResultDto.</returns>
        PersonResultDto Map(Person entity);

        /// <summary>
        /// Maps a PersonRequestDto to a Person entity.
        /// </summary>
        /// <param name="dto">The PersonRequestDto.</param>
        /// <returns>The Person entity.</returns>
        Person Map(PersonRequestDto dto);
    }
}