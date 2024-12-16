using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class UpdatePhoneNumberDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new UpdatePhoneNumberDto
                {
                    PhoneNumber = "+1234567890"
                }
            };
        }
    }
}