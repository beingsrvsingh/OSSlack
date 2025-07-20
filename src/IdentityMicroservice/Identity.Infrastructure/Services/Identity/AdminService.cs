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
        public async Task AddCountryRange(List<CountryMaster> request)
        {
            await repository.CountryMasterRepository.AddRangeAsync(request.ToArray());
            await repository.CountryMasterRepository.SaveChangesAsync();
        }

        public async Task AddCityRange(List<CityMaster> request)
        {
            await repository.CityMasterRepository.AddRangeAsync(request.ToArray());
            await repository.CityMasterRepository.SaveChangesAsync();
        }

        public async Task AddStateRange(List<StateMaster> request)
        {
            await repository.StateMasterRepository.AddRangeAsync(request.ToArray());
            await repository.StateMasterRepository.SaveChangesAsync();
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
