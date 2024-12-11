using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories
{
    /// <summary>
    /// Repository for managing picture entities.
    /// </summary>
    public class PictureRepository : IPictureRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public PictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a picture by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the picture.</param>
        /// <returns>The picture entity.</returns>
        public async Task<Picture> GetPictureByIdAsync(Guid id)
        {
            return await _context.Pictures.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Adds a new picture to the database.
        /// </summary>
        /// <param name="picture">The picture entity to add.</param>
        public async Task AddPictureAsync(Picture picture)
        {
            await _context.Pictures.AddAsync(picture);
        }

        /// <summary>
        /// Deletes a picture from the database.
        /// </summary>
        /// <param name="picture">The picture entity to delete.</param>
        public async Task DeletePictureAsync(Picture picture)
        {
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing picture in the database.
        /// </summary>
        /// <param name="picture">The picture entity to update.</param>
        public async Task UpdatePictureAsync(Picture picture)
        {
            _context.Pictures.Update(picture);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}