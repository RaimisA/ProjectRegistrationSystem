using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class AddressRequestDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new AddressRequestDto
                {
                    City = "City",
                    Street = "Street",
                    HouseNumber = "123",
                    ApartmentNumber = "1"
                }
            };
        }
    }
}