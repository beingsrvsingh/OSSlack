using MediatR;
using SecretManagement.Application.Features.Queries;
using System.Net.Http.Headers;

namespace SecretManagement.Application.Features.QueriesHandler
{
    internal class GetUserSecurityTokenQueryHandler : IRequestHandler<GetUserSecurityTokenQuery, String>
    {
        private readonly HttpClient httpClient;
        public GetUserSecurityTokenQueryHandler()
        {
            httpClient = new HttpClient();
        }
        public async Task<String> Handle(GetUserSecurityTokenQuery request, CancellationToken cancellationToken)
        {

            // TODO Configuration
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWJjQGVtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNzA2MTA3NTg4LCJpc3MiOiJPU1NsYWNrIiwiYXVkIjoiT1NTbGFjayJ9.F-rlx4TBgwdoWbKYc1PkC6iZB12kOD4P7Ys9OowgiMQ");
            var response = await httpClient.PostAsync("https://localhost:7190/api/v1/security-token", new StringContent(request.UserId));

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            throw new HttpRequestException("Token service is unavailable");
        }
    }
}

