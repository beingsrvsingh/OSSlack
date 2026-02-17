using Cart.Application.Services;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;

namespace Cart.Infrastructure.Services
{
    public class PricingClient : IPricingClient
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ILoggerService<PricingClient> _logger;

        public PricingClient(
            IHttpClientService httpClientService,
            ILoggerService<PricingClient> logger)
        {
            _httpClientService = httpClientService;
            _logger = logger;
        }

        public async Task<decimal> GetPriceByProductId(int productId, Microservice microservice)
        {
            try
            {
                var price = await _httpClientService.GetAsync<decimal>(Microservice.Product, $"products/{productId}/price");

                return price;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving price for product {ProductId}", productId);
                throw;
            }
        }
    }
}
