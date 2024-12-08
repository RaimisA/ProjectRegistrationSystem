using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Services
{
    public interface IPictureService
    {
        Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto);
    }
}