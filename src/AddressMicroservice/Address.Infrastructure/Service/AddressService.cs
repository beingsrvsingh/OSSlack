using Address.Application.Features.Commands;
using Address.Application.Service;
using Address.Domain.Core.Repositories;
using Address.Domain.Entities;
using Address.Domain.Enums;
using Mapster;
using Shared.Application.Interfaces.Logging;

namespace Address.Infrastructure.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly ILoggerService<AddressService> _logger;

        public AddressService(IAddressRepository repository, ILoggerService<AddressService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AddressEntity?> GetByOwnerAsync(int ownerId)
        {
            try
            {
                return await _repository.GetByOwnerAsync(ownerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch address for OwnerId: {OwnerId}", ownerId);
                return null;
            }
        }


        public async Task<IEnumerable<AddressEntity>> GetAllByOwnerAsync(int ownerId, AddressOwnerType ownerType)
        {
            try
            {
                return await _repository.GetAllByOwnerAsync(ownerId, ownerType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch addresses.");
                return Enumerable.Empty<AddressEntity>();
            }
        }

        public async Task<AddressEntity?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch address by UID: {id}");
                return null;
            }
        }

        public async Task<AddressEntity> CreateAsync(CreateAddressCommand request)
        {
            try
            {
                var address = request.Adapt<AddressEntity>();
                return await _repository.AddAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create address.");
                throw;
            }
        }

        public async Task<AddressEntity?> UpdateAsync(int id, AddressEntity updated)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return null;

                // Map fields (a better approach is to use AutoMapper)
                existing.Name = updated.Name;
                existing.AddressLine1 = updated.AddressLine1;
                existing.AddressLine2 = updated.AddressLine2;
                existing.City = updated.City;
                existing.State = updated.State;
                existing.Country = updated.Country;
                existing.Pincode = updated.Pincode;
                existing.Landmark = updated.Landmark;
                existing.PhoneNumber = updated.PhoneNumber;
                existing.AddressType = updated.AddressType;
                existing.IsDefault = updated.IsDefault;
                existing.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(existing);
                await _repository.SaveChangesAsync();
                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update address: {id}");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    return false;
                }
                await _repository.DeleteAsync(existing);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete address: {id}");
                return false;
            }
        }
    }
}