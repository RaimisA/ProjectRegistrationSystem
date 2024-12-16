using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Data.Entities;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class PictureSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Picture))
            {
                return new Picture
                {
                    Id = Guid.NewGuid(),
                    FileName = context.Create<string>(),
                    Data = context.Create<byte[]>(),
                    ContentType = context.Create<string>(),
                    Width = context.Create<int>(),
                    Height = context.Create<int>()
                };
            }

            return new NoSpecimen();
        }
    }
}