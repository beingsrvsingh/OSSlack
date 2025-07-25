using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Identity.Infrastructure.Services.Identity
{
    public class AdminService : IAdminService
    {
        private readonly ILoggerService<AdminService> _logger;
        private readonly IUnitOfWork repository;

        public AdminService(ILoggerService<AdminService> logger, IUnitOfWork repository)
        {
            this._logger = logger;
            this.repository = repository;
        }
        public async Task<bool> AddCountryRange(List<CountryMaster> request)
        {
            try
            {
                await repository.CountryMasterRepository.AddRangeAsync(request.ToArray());
                await repository.CountryMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add country range");
                return false;
            }
        }

        public async Task<bool> AddCityRange(List<CityMaster> request)
        {
            try
            {
                await repository.CityMasterRepository.AddRangeAsync(request.ToArray());
                await repository.CityMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add city range");
                return false;
            }
        }

        public async Task<bool> AddStateRange(List<StateMaster> request)
        {
            try
            {
                await repository.StateMasterRepository.AddRangeAsync(request.ToArray());
                await repository.StateMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add state range");
                return false;
            }
        }
        public async Task<StateMaster?> GetStateByName(string name)
        {
            return await repository.StateMasterRepository.GetBy(state => state.Name.Contains(name));
        }

        public async Task<CountryMaster?> GetCountryByNameAsync(string countryName)
        {
            return await repository.CountryMasterRepository.GetBy(count => count.Name.Contains(countryName));
        }
    }
}
