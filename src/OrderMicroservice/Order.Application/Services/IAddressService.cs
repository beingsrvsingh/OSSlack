using Order.Application.Contracts;

namespace Order.Application.Services
{
    public interface IAddressService
    {
        Task<ShippingInfoDto?> GetAddressInfoByIdAsync(int addressId);
    }
}