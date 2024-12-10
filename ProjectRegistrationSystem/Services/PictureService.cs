using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services
{
    public class PictureService : IPictureService
    {
        private readonly ApplicationDbContext _context;

        public PictureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Picture> ProcessAndSavePictureAsync(PictureRequestDto pictureRequestDto, Guid? existingPictureId = null)
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

            Picture picture;
            if (existingPictureId.HasValue)
            {
                picture = await _context.Pictures.FindAsync(existingPictureId.Value);
                if (picture != null)
                {
                    picture.FileName = pictureRequestDto.FileName;
                    picture.Data = resizedImageData;
                    picture.ContentType = pictureRequestDto.ContentType;
                    picture.Width = image.Width;
                    picture.Height = image.Height;
                    _context.Pictures.Update(picture);
                }
                else
                {
                    picture = new Picture
                    {
                        Id = Guid.NewGuid(),
                        FileName = pictureRequestDto.FileName,
                        Data = resizedImageData,
                        ContentType = pictureRequestDto.ContentType,
                        Width = image.Width,
                        Height = image.Height
                    };
                    _context.Pictures.Add(picture);
                }
            }
            else
            {
                picture = new Picture
                {
                    Id = Guid.NewGuid(),
                    FileName = pictureRequestDto.FileName,
                    Data = resizedImageData,
                    ContentType = pictureRequestDto.ContentType,
                    Width = image.Width,
                    Height = image.Height
                };
                _context.Pictures.Add(picture);
            }

            await _context.SaveChangesAsync();
            return picture;
        }
    }
}