using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class UpdatePersonalCodeDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new UpdatePersonalCodeDto
                {
                    PersonalCode = "12345678901"
                }
            };
        }
    }
}