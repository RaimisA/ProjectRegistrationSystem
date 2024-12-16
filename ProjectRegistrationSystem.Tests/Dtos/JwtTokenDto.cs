using System;

namespace ProjectRegistrationSystem.Tests.Dtos
{
    public class JwtTokenDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }
    }
}