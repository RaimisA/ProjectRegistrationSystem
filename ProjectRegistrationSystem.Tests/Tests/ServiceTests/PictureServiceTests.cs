using AutoFixture;
using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Services;
using ProjectRegistrationSystem.Tests.DataAttributes;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ProjectRegistrationSystem.Tests.ServiceTests
{
    public class PictureServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly PictureService _pictureService;
        private readonly IFixture _fixture;

        public PictureServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _pictureService = new PictureService(_context);

            _fixture = new Fixture();
            _fixture.Customizations.Add(new PictureRequestDtoSpecimenBuilder());
        }

        [Theory]
        [ClassData(typeof(PictureRequestDtoDataAttribute))]
        public async Task ProcessAndSavePictureAsync_NewPicture_ReturnsPicture(PictureRequestDto pictureRequestDto)
        {
            // Act
            var result = await _pictureService.ProcessAndSavePictureAsync(pictureRequestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pictureRequestDto.FileName, result.FileName);
            Assert.Equal(pictureRequestDto.ContentType, result.ContentType);
            Assert.Equal(200, result.Width);
            Assert.Equal(200, result.Height);
        }

        [Theory]
        [ClassData(typeof(PictureRequestDtoDataAttribute))]
        public async Task ProcessAndSavePictureAsync_ExistingPicture_ReturnsUpdatedPicture(PictureRequestDto pictureRequestDto)
        {
            // Arrange
            var existingPicture = new Picture
            {
                Id = Guid.NewGuid(),
                FileName = "existing.jpg",
                Data = File.ReadAllBytes("TestImages/test.jpg"),
                ContentType = "image/jpeg",
                Width = 100,
                Height = 100
            };
            _context.Pictures.Add(existingPicture);
            await _context.SaveChangesAsync();

            // Act
            var result = await _pictureService.ProcessAndSavePictureAsync(pictureRequestDto, existingPicture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pictureRequestDto.FileName, result.FileName);
            Assert.Equal(pictureRequestDto.ContentType, result.ContentType);
            Assert.Equal(200, result.Width);
            Assert.Equal(200, result.Height);
        }
    }
}