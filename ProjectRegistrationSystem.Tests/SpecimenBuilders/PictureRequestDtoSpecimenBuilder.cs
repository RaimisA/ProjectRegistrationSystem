using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Dtos.Requests;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class PictureRequestDtoSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(PictureRequestDto))
            {
                return new PictureRequestDto
                {
                    FileName = context.Create<string>() + ".jpg",
                    Data = context.Create<byte[]>(),
                    ContentType = "image/jpeg"
                };
            }

            return new NoSpecimen();
        }
    }
}