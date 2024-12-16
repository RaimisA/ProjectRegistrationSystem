using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;

namespace ProjectRegistrationSystem.Services.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto, Guid? existingPictureId = null);
    }
}