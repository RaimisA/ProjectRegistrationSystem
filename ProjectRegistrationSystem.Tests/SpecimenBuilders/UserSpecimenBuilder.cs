using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Data.Entities;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class UserSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(User))
            {
                return new User
                {
                    Id = Guid.NewGuid(),
                    Username = context.Create<string>(),
                    PasswordHash = Convert.ToBase64String(context.Create<byte[]>()),
                    PasswordSalt = Convert.ToBase64String(context.Create<byte[]>()),
                    Role = "User",
                    Person = context.Create<Person>()
                };
            }

            return new NoSpecimen();
        }
    }
}