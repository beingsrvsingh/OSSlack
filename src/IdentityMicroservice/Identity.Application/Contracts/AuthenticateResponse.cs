using Newtonsoft.Json;

namespace Identity.Application.Contracts
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string AccessToken { get; init; }

        // refresh token is returned in http only cookie
        [JsonIgnore]
        public string RefreshToken { get; init; }

        public int ExpiresIn { get; init; }

        public AuthenticateResponse(string id, string accessToken, string refreshToken, int expiresIn)
        {
            Id = id;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }
    }
}
