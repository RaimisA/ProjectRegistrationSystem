using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    /// <summary>
    /// Mapper for converting between Picture entities and DTOs.
    /// </summary>
    public class PictureMapper : IPictureMapper
    {
        /// <summary>
        /// Maps a PictureRequestDto to a Picture entity.
        /// </summary>
        /// <param name="dto">The PictureRequestDto.</param>
        /// <returns>The Picture entity.</returns>
        public Picture Map(PictureRequestDto dto)
        {
            return new Picture
            {
                FileName = dto.FileName,
                Data = dto.Data,
                ContentType = dto.ContentType
            };
        }

        /// <summary>
        /// Maps a Picture entity to a PictureResultDto.
        /// </summary>
        /// <param name="entity">The Picture entity.</param>
        /// <returns>The PictureResultDto.</returns>
        public PictureResultDto Map(Picture entity)
        {
            return new PictureResultDto
            {
                Id = entity.Id,
                FileName = entity.FileName,
                Data = entity.Data,
                ContentType = entity.ContentType
            };
        }
    }
}