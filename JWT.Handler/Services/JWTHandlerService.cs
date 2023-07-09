namespace JWT.Handler.Services
{
    internal class JWTHandlerService : IJWTHandler
    {
        public string GenerateJWT()
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
        public string GenerateJWT();
        public bool ValidateToken();
    }
}
