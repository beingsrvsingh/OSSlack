using Address.Domain.Entities;

namespace Address.Application.Service
{
    public interface ISeedService
    {
        Task<bool> SeedAddressAsync(List<AddressEntity> addressEntities, List<AddressType> addressTypes);
    }
}