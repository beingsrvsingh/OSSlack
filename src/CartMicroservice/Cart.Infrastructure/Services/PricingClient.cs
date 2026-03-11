using Cart.Application.Services;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Entities.Base;
using Shared.Domain.Enums;
using Shared.Utilities.Response;

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
                var response = await _httpClientService.GetAsync<Result<decimal?>> (Microservice.Product, $"product/{productId}/price");

                return response?.Data ?? 0m;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving price for product {ProductId}", productId);
                throw;
            }
        }

        public async Task<BasePrice> GetPriceByPriestExpertiseIdAndModeId(int priestExpertiseId, int modeId, Microservice microservice)
        {
            try
            {
                var response = await _httpClientService.GetAsync<Result<BasePrice>>(Microservice.Priest, $"priest/{priestExpertiseId}/{modeId}/price");

                return response?.Data ?? new BasePrice();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving price for priest {priestExpertiseId}", priestExpertiseId);
                throw;
            }
        }
    }
}
