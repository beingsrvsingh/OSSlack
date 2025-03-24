using Review.Application.Contracts;
using Review.Application.Services;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Review.Infrastructure.Services
{
    public class IdentityApiClient : IIdentityApiClient
    {
        private readonly HttpClient httpClient;

        public IdentityApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GetUserReponse?> GetUserAsync(object request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/v1/user")
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            var response = await httpClient.SendAsync(httpRequest);

            GetUserReponse? userResponse = new();
            if (response.IsSuccessStatusCode)
            {
                userResponse = await response.Content.ReadFromJsonAsync<GetUserReponse>()!;
            }

            return userResponse;
        }
    }
}
