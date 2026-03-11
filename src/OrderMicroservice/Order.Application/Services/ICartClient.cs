using Order.Application.Contracts;

namespace Order.Application.Services
{
    public interface ICartClient
    {
        Task<CartDto?> GetCartInfoByUserIdAsync(string userId);
    }
}
