using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.RepositoryTests
{
    public class PictureRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PictureRepository _pictureRepository;

        public PictureRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _pictureRepository = new PictureRepository(_context);
        }

        [Theory, PictureData]
        public async Task GetPictureByIdAsync_ValidId_ReturnsPicture(Picture picture)
        {
            // Arrange
            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();

            // Act
            var result = await _pictureRepository.GetPictureByIdAsync(picture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(picture.Id, result.Id);
        }

        [Theory, PictureData]
        public async Task AddPictureAsync_ValidPicture_AddsPicture(Picture picture)
        {
            // Act
            await _pictureRepository.AddPictureAsync(picture);
            await _pictureRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Pictures.FindAsync(picture.Id);
            Assert.NotNull(result);
            Assert.Equal(picture.Id, result.Id);
        }

        [Theory, PictureData]
        public async Task DeletePictureAsync_ValidPicture_DeletesPicture(Picture picture)
        {
            // Arrange
            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();

            // Act
            await _pictureRepository.DeletePictureAsync(picture);

            // Assert
            var result = await _context.Pictures.FindAsync(picture.Id);
            Assert.Null(result);
        }

        [Theory, PictureData]
        public async Task UpdatePictureAsync_ValidPicture_UpdatesPicture(Picture picture)
        {
            // Arrange
            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();

            // Act
            picture.FileName = "updated.jpg";
            await _pictureRepository.UpdatePictureAsync(picture);

            // Assert
            var result = await _context.Pictures.FindAsync(picture.Id);
            Assert.NotNull(result);
            Assert.Equal("updated.jpg", result.FileName);
        }

        [Theory, PictureData]
        public async Task SaveChangesAsync_SavesChanges(Picture picture)
        {
            // Arrange
            await _context.Pictures.AddAsync(picture);

            // Act
            await _pictureRepository.SaveChangesAsync();

            // Assert
            var result = await _context.Pictures.FindAsync(picture.Id);
            Assert.NotNull(result);
            Assert.Equal(picture.Id, result.Id);
        }
    }
}