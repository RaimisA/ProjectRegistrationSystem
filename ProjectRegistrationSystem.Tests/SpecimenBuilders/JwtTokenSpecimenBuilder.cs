using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Tests.Dtos;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class JwtTokenSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(JwtTokenDto))
            {
                return new JwtTokenDto
                {
                    Username = context.Create<string>(),
                    Role = context.Create<string>(),
                    UserId = context.Create<Guid>()
                };
            }

            return new NoSpecimen();
        }
    }
}