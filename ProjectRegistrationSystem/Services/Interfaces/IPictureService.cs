using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Services.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto);
    }
}