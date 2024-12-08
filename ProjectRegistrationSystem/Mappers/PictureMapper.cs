using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    public class PictureMapper : IPictureMapper
    {
        public Picture Map(PictureRequestDto dto)
        {
            return new Picture
            {
                FileName = dto.FileName,
                Data = dto.Data,
                ContentType = dto.ContentType
            };
        }

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