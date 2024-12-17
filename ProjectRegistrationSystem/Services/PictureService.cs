using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureService"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Processes and saves a picture.
        /// </summary>
        /// <param name="pictureRequestDto">The picture request DTO.</param>
        /// <param name="existingPictureId">The optional existing picture ID to update.</param>
        /// <returns>The processed and saved picture entity.</returns>
        public async Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto, Guid? existingPictureId = null)
        {
            try
            {
                using var memoryStream = new MemoryStream(pictureRequestDto.Data);
                IImageFormat format = Image.DetectFormat(memoryStream);
                if (format == null)
                {
                    throw new InvalidOperationException("The provided image format is not supported.");
                }

                memoryStream.Position = 0; // Reset stream position after format detection
                using var image = Image.Load(memoryStream);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(200, 200),
                    Mode = ResizeMode.Crop
                }));

                using var resizedStream = new MemoryStream();
                image.Save(resizedStream, new JpegEncoder());
                var resizedImageData = resizedStream.ToArray();

                Picture picture;
                if (existingPictureId.HasValue)
                {
                    picture = await _context.Pictures.FindAsync(existingPictureId.Value) ?? new Picture();
                    picture.FileName = pictureRequestDto.FileName;
                    picture.Data = resizedImageData;
                    picture.ContentType = pictureRequestDto.ContentType;
                    picture.Width = image.Width;
                    picture.Height = image.Height;
                    _context.Pictures.Update(picture);
                }
                else
                {
                    picture = CreateNewPicture(pictureRequestDto, resizedImageData, image);
                    _context.Pictures.Add(picture);
                }

                await _context.SaveChangesAsync();
                return picture;
            }
            catch (UnknownImageFormatException ex)
            {
                throw new InvalidOperationException("The provided image format is not supported.", ex);
            }
        }

        private static Picture CreateNewPicture(PictureRequestDto pictureRequestDto, byte[] resizedImageData, Image image)
        {
            return new Picture
            {
                Id = Guid.NewGuid(),
                FileName = pictureRequestDto.FileName,
                Data = resizedImageData,
                ContentType = pictureRequestDto.ContentType,
                Width = image.Width,
                Height = image.Height
            };
        }
    }
}