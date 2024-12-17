using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    /// <summary>
    /// Interface for mapping picture entities and DTOs.
    /// </summary>
    public interface IPictureMapper
    {
        /// <summary>
        /// Maps a Picture entity to a PictureResultDto.
        /// </summary>
        /// <param name="entity">The Picture entity.</param>
        /// <returns>The PictureResultDto.</returns>
        PictureResultDto Map(Picture entity);

        /// <summary>
        /// Maps a PictureRequestDto to a Picture entity.
        /// </summary>
        /// <param name="dto">The PictureRequestDto.</param>
        /// <returns>The Picture entity.</returns>
        Picture Map(PictureRequestDto dto);
    }
}