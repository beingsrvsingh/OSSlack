using Address.Application.Service;
using Address.Domain.Core.Repositories;
using Address.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Address.Infrastructure.Service
{
    public class SeedService : ISeedService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IAddressTypeRepository addressTypeRepository;
        private readonly ILoggerService<SeedService> _logger;
        public SeedService(ILoggerService<SeedService> loggerService, IAddressRepository addressRepository,
        IAddressTypeRepository addressTypeRepository)
        {
            this._logger = loggerService;
            this.addressRepository = addressRepository;
            this.addressTypeRepository = addressTypeRepository;
        }

        public async Task<bool> SeedAddressAsync(List<AddressEntity> addressEntities, List<AddressType> addressTypes)
        {
            using var transaction = await addressTypeRepository.BeginTransactionAsync();
            try
            {
                foreach (var address in addressTypes)
                {
                    bool exists = await addressTypeRepository.AnyAsync(a => a.Id == address.Id);
                    if (!exists)
                    {
                        await addressTypeRepository.AddAsync(address);
                    }
                }

                await addressTypeRepository.SaveChangesAsync();

                foreach (var address in addressEntities)
                {
                    bool exists = await addressRepository.AnyAsync(a => a.Uid == address.Uid);
                    if (!exists)
                    {
                        await addressRepository.AddAsync(address);
                    }
                }

                await addressRepository.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while seeding address data.");
                return false;
            }
        }
    }
}