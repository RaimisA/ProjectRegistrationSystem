using AutoFixture;
using AutoFixture.Kernel;
using ProjectRegistrationSystem.Data.Entities;
using System;

namespace ProjectRegistrationSystem.Tests.SpecimenBuilders
{
    public class PersonSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Person))
            {
                var user = context.Create<User>();
                user.Person = null;

                return new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = context.Create<string>(),
                    LastName = context.Create<string>(),
                    PersonalCode = context.Create<string>(),
                    PhoneNumber = context.Create<string>(),
                    Email = context.Create<string>(),
                    Address = context.Create<Address>(),
                    UserId = user.Id,
                    User = user,
                    ProfilePicture = context.Create<Picture>()
                };
            }

            return new NoSpecimen();
        }
    }
}