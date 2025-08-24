using Address.Domain.Entities;
using Address.Domain.Enums;
using Shared.Domain.Repository;

namespace Address.Domain.Core.Repositories
{
    public interface IAddressRepository : IRepository<AddressEntity>
    {
        Task<IEnumerable<AddressEntity>> GetByOwnerAsync(int ownerId, AddressOwnerType ownerType);
    }
}