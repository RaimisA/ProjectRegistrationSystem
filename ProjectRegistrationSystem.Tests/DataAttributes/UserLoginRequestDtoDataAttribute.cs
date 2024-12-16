using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class UserLoginRequestDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new UserLoginRequestDto
                {
                    Username = "testuser",
                    Password = "Test@1234"
                }
            };
            yield return new object[]
            {
                new UserLoginRequestDto
                {
                    Username = "anotheruser",
                    Password = "Another@1234"
                }
            };
        }
    }
}