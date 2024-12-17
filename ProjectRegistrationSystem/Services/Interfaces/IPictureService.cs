using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;

namespace ProjectRegistrationSystem.Services.Interfaces
{
    /// <summary>
    /// Interface for processing and saving pictures.
    /// </summary>
    public interface IPictureService
    {
        /// <summary>
        /// Processes and saves a picture.
        /// </summary>
        /// <param name="pictureRequestDto">The picture request DTO.</param>
        /// <param name="existingPictureId">The optional existing picture ID to update.</param>
        /// <returns>The processed and saved picture entity.</returns>
        Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto, Guid? existingPictureId = null);
    }
}