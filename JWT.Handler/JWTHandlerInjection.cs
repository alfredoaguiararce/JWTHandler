using JWT.Handler.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JWT.Handler
{
    public static class JWTHandlerInjection
    {
        /// <summary>
        /// The function extends the IServiceCollection interface in C# to add a scoped JWTHandler
        /// service.
        /// </summary>
        /// <param name="IServiceCollection">The IServiceCollection interface is used to define a
        /// contract for a collection of service descriptors. It is typically used to register and
        /// configure services in an application's dependency injection container.</param>
        public static void InjectJWTHandler(this IServiceCollection services)
        {
            services.AddScoped<IJWTHandler>(service => new JWTHandlerService());
        }
    }
}