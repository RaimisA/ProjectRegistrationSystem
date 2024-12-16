using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using ProjectRegistrationSystem.Services;
using ProjectRegistrationSystem.Tests.DataAttributes;
using ProjectRegistrationSystem.Tests.Dtos;
using ProjectRegistrationSystem.Tests.SpecimenBuilders;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace ProjectRegistrationSystem.Tests.ServiceTests
{
    public class JwtServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly JwtService _jwtService;
        private readonly IFixture _fixture;

        public JwtServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c.GetSection("Jwt:Key").Value).Returns("supersecretkey123456789012345678901234567890123456789012345678901234567890");
            _mockConfiguration.Setup(c => c.GetSection("Jwt:Issuer").Value).Returns("TestIssuer");
            _mockConfiguration.Setup(c => c.GetSection("Jwt:Audience").Value).Returns("TestAudience");

            _jwtService = new JwtService(_mockConfiguration.Object);

            _fixture = new Fixture();
            _fixture.Customizations.Add(new JwtTokenSpecimenBuilder());
        }

        [Theory]
        [ClassData(typeof(JwtTokenDataAttribute))]
        public void GetJwtToken_ValidData_ReturnsToken(string username, string role, Guid userId)
        {
            // Act
            var token = _jwtService.GetJwtToken(username, role, userId);

            // Assert
            Assert.NotNull(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            Assert.Equal(username, jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            Assert.Equal(role, jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value);
            Assert.Equal(userId.ToString(), jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Assert.Equal("TestIssuer", jwtToken.Issuer);
            Assert.Equal("TestAudience", jwtToken.Audiences.First());
        }
    }
}