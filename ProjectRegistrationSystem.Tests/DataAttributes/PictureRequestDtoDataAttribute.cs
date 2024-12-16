using ProjectRegistrationSystem.Dtos.Requests;
using SixLabors.ImageSharp;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class PictureRequestDtoDataAttribute : TheoryData<PictureRequestDto>
    {
        public PictureRequestDtoDataAttribute()
        {
            EnsureTestImagesExist();

            Add(new PictureRequestDto
            {
                FileName = "test.jpg",
                Data = File.ReadAllBytes("TestImages/test.jpg"),
                ContentType = "image/jpeg"
            });
            Add(new PictureRequestDto
            {
                FileName = "test.png",
                Data = File.ReadAllBytes("TestImages/test.png"),
                ContentType = "image/png"
            });
        }

        private void EnsureTestImagesExist()
        {
            Directory.CreateDirectory("TestImages");

            if (!File.Exists("TestImages/test.jpg"))
            {
                File.WriteAllBytes("TestImages/test.jpg", GenerateValidImageBytes("image/jpeg"));
            }

            if (!File.Exists("TestImages/test.png"))
            {
                File.WriteAllBytes("TestImages/test.png", GenerateValidImageBytes("image/png"));
            }
        }

        private byte[] GenerateValidImageBytes(string contentType)
        {
            using var image = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(1, 1);
            using var memoryStream = new MemoryStream();
            if (contentType == "image/jpeg")
            {
                image.SaveAsJpeg(memoryStream);
            }
            else if (contentType == "image/png")
            {
                image.SaveAsPng(memoryStream);
            }
            return memoryStream.ToArray();
        }
    }
}