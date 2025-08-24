
using Address.Application.Features.Commands;
using Address.Domain.Entities;
using Address.Domain.Enums;

namespace Address.Application.Service
{
    public interface IAddressService
    {
        Task<AddressEntity?> GetByOwnerAsync(int ownerId);
        Task<IEnumerable<AddressEntity>> GetAllByOwnerAsync(int ownerId, AddressOwnerType ownerType);
        Task<AddressEntity?> GetByIdAsync(int id);
        Task<AddressEntity> CreateAsync(CreateAddressCommand request);
        Task<AddressEntity?> UpdateAsync(int id, AddressEntity updated);
        Task<bool> DeleteAsync(int id);
        Task<bool> MarkAddressAsDefaultAsync(int addressId);
    }
}