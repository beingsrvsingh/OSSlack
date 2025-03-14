using Shared.Application.Common.Services.Interfaces;
using Shared.Domain.Entities;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Shared.Utilities.Services
{
    public class LoggingApiClient : ILoggingApiClient
    {
        private readonly HttpClient httpClient;

        public LoggingApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddLog(Log request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/logger/log")
            {
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json),
            };

            await httpClient.SendAsync(httpRequest);

            await Task.CompletedTask;

        }
    }
}
