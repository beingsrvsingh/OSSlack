using Address.Domain.Entities;
using Address.Domain.Enums;
using Shared.Domain.Repository;

namespace Address.Domain.Core.Repositories
{
    public interface IAddressRepository : IRepository<AddressEntity>
    {
        Task<IEnumerable<AddressEntity>> GetAllByOwnerAsync(int ownerId, AddressOwnerType ownerType);
        Task<AddressEntity?> GetByOwnerAsync(int ownerId);
    }
}