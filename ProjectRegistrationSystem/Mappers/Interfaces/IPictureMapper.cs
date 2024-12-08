using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Mappers.Interfaces
{
    public interface IPictureMapper
    {
        PictureResultDto Map(Picture entity);
        Picture Map(PictureRequestDto dto);
    }
}