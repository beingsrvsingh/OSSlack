using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;

namespace Identity.Infrastructure.Services.Identity
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork repository;

        public AdminService(IUnitOfWork repository)
        {
            this.repository = repository;
        }
        public Task AddCountryRange(List<CountryMaster> request)
        {
            repository.CountryMasterRepository.AddRangeAsync(request.ToArray());
            repository.CountryMasterRepository.Save();
            return Task.CompletedTask;
        }

        public Task AddCityRange(List<CityMaster> request)
        {
            repository.CityMasterRepository.AddRangeAsync(request.ToArray());
            repository.CityMasterRepository.Save();
            return Task.CompletedTask;
        }

        public Task AddStateRange(List<StateMaster> request)
        {
            repository.StateMasterRepository.AddRangeAsync(request.ToArray());
            repository.StateMasterRepository.Save();
            return Task.CompletedTask;
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
