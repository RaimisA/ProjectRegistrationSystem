using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class PersonRequestDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new PersonRequestDto
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PersonalCode = "12345678901",
                    PhoneNumber = "+1234567890",
                    Email = "john.doe@example.com",
                    Address = new AddressRequestDto
                    {
                        City = "City",
                        Street = "Street",
                        HouseNumber = "123",
                        ApartmentNumber = "1"
                    }
                }
            };
        }
    }
}