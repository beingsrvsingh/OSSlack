using Identity.Application.Contracts;

namespace Identity.Infrastructure.Services.Identity
{
    public class Tokens
    {
        public static AuthenticateResponse GenerateJwt(string Id, string accessToken, string refreshToken, int accessTokenExpiresIn)
        {
            return new AuthenticateResponse(Id, accessToken, refreshToken, accessTokenExpiresIn);
        }
    }
}
