using JWT.Handler.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWT.Handler.Tests
{
    [TestFixture]
    public class JWTHandlerInjectionTests
    {
        [Test]
        public void InjectJWTHandler_Should_Register()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.InjectJWTHandler();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var jwtHandler = serviceProvider.GetService<IJWTHandler>();

            Assert.IsNotNull(jwtHandler);
        }

        [Test]
        public void InjectJWTHandler_JWTHandlerService_AsScoped()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.InjectJWTHandler();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var jwtHandler = serviceProvider.GetService<IJWTHandler>();

            Assert.That(jwtHandler, Is.InstanceOf<IJWTHandler>());
        }
        
    }
}
