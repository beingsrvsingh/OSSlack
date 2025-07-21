using Polly.Wrap;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Entities;
using Shared.Infrastructure.Http;
using Shared.Infrastructure.Resilience;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Shared.Utilities.Services
{
    public class LoggingApiClient : ILoggingApiClient
    {
        private readonly HttpClient httpClient;
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;

        public LoggingApiClient(IHttpClientWrapper clientWrapper)
        {
            this.httpClient = clientWrapper.CreateClient();
            _policyWrap = PollyPolicyProvider.CreatePolicy(); // uses default values

        }

        public async Task AddLogAsync(BaseLog request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/logger/log")
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            var response = await _policyWrap.ExecuteAsync(() => httpClient.SendAsync(httpRequest));
            response.EnsureSuccessStatusCode();

        }
    }
}
