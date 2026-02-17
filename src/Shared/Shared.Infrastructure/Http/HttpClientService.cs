using Microsoft.Extensions.Logging;
using Shared.Application.Interfaces;
using Shared.Domain.Enums;
using System.Net.Http;
using System.Net.Http.Json;

namespace Shared.Infrastructure.Http
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(
            IHttpClientFactory httpClientFactory,
            ILogger<HttpClientService> logger)
        {
            _httpClientFactory = httpClientFactory
                ?? throw new ArgumentNullException(nameof(httpClientFactory));

            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse?> GetAsync<TResponse>(
            Microservice microservice,
            string endpoint,
            CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(microservice.ToString());

            var response = await client.GetAsync(endpoint, cancellationToken);

            await EnsureSuccessAsync(response);

            return await response.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(
            Microservice microservice,
            string endpoint,
            TRequest data,
            CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(microservice.ToString());

            var response = await client.PostAsJsonAsync(endpoint, data, cancellationToken);

            await EnsureSuccessAsync(response);

            return await response.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(
            Microservice microservice,
            string endpoint,
            TRequest data,
            CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(microservice.ToString());

            var response = await client.PutAsJsonAsync(endpoint, data, cancellationToken);

            await EnsureSuccessAsync(response);

            return await response.Content
                .ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
        }

        public async Task<bool> DeleteAsync(
            Microservice microservice,
            string endpoint,
            CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(microservice.ToString());

            var response = await client.DeleteAsync(endpoint, cancellationToken);

            await EnsureSuccessAsync(response);

            return response.IsSuccessStatusCode;
        }

        private async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                _logger.LogError(
                    "HTTP request failed. StatusCode: {StatusCode}, Response: {Response}",
                    response.StatusCode,
                    content);

                throw new HttpRequestException(
                    $"Request failed with status code {response.StatusCode}");
            }
        }
    }

}
