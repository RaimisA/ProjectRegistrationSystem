using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Data.Entities;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class AddressSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Address))
            {
                return new Address
                {
                    Id = Guid.NewGuid(),
                    City = context.Create<string>(),
                    Street = context.Create<string>(),
                    HouseNumber = context.Create<string>(),
                    ApartmentNumber = context.Create<string>()
                };
            }

            return new NoSpecimen();
        }
    }
}