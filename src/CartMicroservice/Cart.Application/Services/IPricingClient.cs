using Shared.Domain.Entities.Base;
using Shared.Domain.Enums;

namespace Cart.Application.Services
{
    public interface IPricingClient
    {
        Task<decimal> GetPriceByProductId(int productId, Microservice microservice);
        Task<BasePrice> GetPriceByPriestExpertiseIdAndModeId(int priestExpertiseId, int modeId, Microservice microservice);
    }
}
