using ProjectRegistrationSystem.Dtos.Requests;
using System.Collections.Generic;
using Xunit.Sdk;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class UserRequestDtoDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            yield return new object[]
            {
                new UserRequestDto
                {
                    Username = "testuser",
                    Password = "Test@1234",
                    ConfirmPassword = "Test@1234"
                }
            };
            yield return new object[]
            {
                new UserRequestDto
                {
                    Username = "anotheruser",
                    Password = "Another@1234",
                    ConfirmPassword = "Another@1234"
                }
            };
        }
    }
}