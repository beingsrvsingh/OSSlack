using Shared.Domain.Enums;

namespace Cart.Application.Services
{
    public interface IPricingClient
    {
        Task<decimal> GetPriceByProductId(int productId, Microservice microservice);
    }
}
