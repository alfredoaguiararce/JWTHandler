using JWT.Handler;
using JWT.Handler.Models;
using JWT.Handler.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace JWTHandler.tests.Services
{
    [TestFixture]
    public class JWTHandlerInjectionTests
    {
        private IJWTHandler? _jwtHandler;
        private string? JWT;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.InjectJWTHandler();

            var serviceProvider = services.BuildServiceProvider();
            _jwtHandler = serviceProvider.GetService<IJWTHandler>();
            // Assert
            Assert.IsNotNull(_jwtHandler);
            Assert.IsInstanceOf<IJWTHandler>(_jwtHandler);
        }

        [Test]
        public void GenerateJWT_Should_Return_Valid_JWT()
        {
            // Arrange
            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                SecretKeySHA256 = "f8c0a127e902ff44685887a0d1e12c0796d5aa7f75766bbec6679c2dadbf84ff"
                ,Audience = "audience"
                ,Issuer = "Issuer"
            };

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Alice"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Act
            JWT = _jwtHandler?.GenerateJWT(configuration, claims);

            // Assert
            Assert.That(JWT, Is.Not.Null);
        }

        [Test]
        public void ValidateToken()
        {
            // Arrange
            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                SecretKeySHA256 = "f8c0a127e902ff44685887a0d1e12c0796d5aa7f75766bbec6679c2dadbf84ff"
                ,
                Audience = "audience"
                ,
                Issuer = "Issuer"
            };

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Alice"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Act
            string JWT = _jwtHandler?.GenerateJWT(configuration, claims);

            // Assert
            Assert.That(JWT, Is.Not.Null);

            bool result = _jwtHandler.ValidateToken(JWT);
            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetClaims()
        {
            // Arrange
            JWTHandlerConfiguration configuration = new JWTHandlerConfiguration
            {
                SecretKeySHA256 = "f8c0a127e902ff44685887a0d1e12c0796d5aa7f75766bbec6679c2dadbf84ff"
                ,
                Audience = "audience"
                ,
                Issuer = "Issuer"
            };

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Alice"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Act
            string JWT = _jwtHandler?.GenerateJWT(configuration, claims);

            // Assert
            Assert.That(JWT, Is.Not.Null);

            ClaimsPrincipal? result = _jwtHandler.GetClaims(JWT);
            // Assert
            Assert.That(result, Is.Not.Null);
        }
    }
}