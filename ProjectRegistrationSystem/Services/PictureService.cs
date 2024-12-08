using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services
{
    /// <summary>
    /// Service for processing and saving pictures.
    /// </summary>
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _context;

        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Processes and saves the picture by resizing it to 200x200 pixels.
        /// </summary>
        /// <param name="pictureRequestDto">The picture request DTO containing the picture data.</param>
        /// <returns>The processed and saved picture entity.</returns>
        public async Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto)
        {
            using var memoryStream = new MemoryStream(pictureRequestDto.Data);
            using var image = Image.Load(memoryStream);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(200, 200),
                Mode = ResizeMode.Crop
            }));

            using var resizedStream = new MemoryStream();
            image.Save(resizedStream, new JpegEncoder());
            var resizedImageData = resizedStream.ToArray();

            var picture = new Picture
            {
                Id = Guid.NewGuid(),
                FileName = pictureRequestDto.FileName,
                Data = resizedImageData,
                ContentType = pictureRequestDto.ContentType,
                Width = image.Width,
                Height = image.Height
            };

            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            return picture;
        }
    }
}