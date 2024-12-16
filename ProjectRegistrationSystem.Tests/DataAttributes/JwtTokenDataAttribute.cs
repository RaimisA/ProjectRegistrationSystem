using System;
using System.Collections.Generic;
using Xunit;

namespace ProjectRegistrationSystem.Tests.DataAttributes
{
    public class JwtTokenDataAttribute : TheoryData<string, string, Guid>
    {
        public JwtTokenDataAttribute()
        {
            Add("testuser1", "User", Guid.NewGuid());
            Add("testuser2", "Admin", Guid.NewGuid());
        }
    }
}