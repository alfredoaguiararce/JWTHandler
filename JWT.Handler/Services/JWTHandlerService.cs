using JWT.Handler.Models;

namespace JWT.Handler.Services
{
    internal class JWTHandlerService : IJWTHandler
    {

        public string GenerateJWT(JWTHandlerConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public bool ValidateToken()
        {
            throw new NotImplementedException();
        }
    }

    public interface IJWTHandler
    {
        public string GenerateJWT(JWTHandlerConfiguration configuration);
        public bool ValidateToken();
    }
}
