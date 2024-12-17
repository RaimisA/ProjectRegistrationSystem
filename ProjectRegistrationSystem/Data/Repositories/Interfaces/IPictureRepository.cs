using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing picture entities.
    /// </summary>
    public interface IPictureRepository
    {
        /// <summary>
        /// Gets a picture by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the picture.</param>
        /// <returns>The picture entity.</returns>
        Task<Picture> GetPictureByIdAsync(Guid id);

        /// <summary>
        /// Adds a new picture to the database.
        /// </summary>
        /// <param name="picture">The picture entity to add.</param>
        Task AddPictureAsync(Picture picture);

        /// <summary>
        /// Deletes a picture from the database.
        /// </summary>
        /// <param name="picture">The picture entity to delete.</param>
        Task DeletePictureAsync(Picture picture);

        /// <summary>
        /// Updates an existing picture in the database.
        /// </summary>
        /// <param name="picture">The picture entity to update.</param>
        Task UpdatePictureAsync(Picture picture);

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        Task SaveChangesAsync();
    }
}