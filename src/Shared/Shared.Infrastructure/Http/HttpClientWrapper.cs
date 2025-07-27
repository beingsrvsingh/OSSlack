using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Utilities;
using Shared.Utilities.Constants;

namespace Shared.Infrastructure.Http
{
    public interface IHttpClientWrapper
    {
        HttpClient CreateClient();
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseApiUrl;

        public HttpClientWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseApiUrl = Constants.LoggerBaseApiGatewayUri;
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseApiUrl)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var context = _httpContextAccessor.HttpContext;
            Utils.AddHeadersToken(client, context);

            return client;
        }
    }
}