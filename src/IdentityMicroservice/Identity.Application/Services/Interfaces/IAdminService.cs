using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<CountryMaster?> GetCountryByNameAsync(string countryName);
        Task<StateMaster?> GetStateByName(string name);
        Task AddCountryRange(List<CountryMaster> request);
        Task AddStateRange(List<StateMaster> request);
        Task AddCityRange(List<CityMaster> request);        
    }
}
