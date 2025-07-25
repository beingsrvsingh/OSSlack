using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<CountryMaster?> GetCountryByNameAsync(string countryName);
        Task<StateMaster?> GetStateByName(string name);
        Task<bool> AddCountryRange(List<CountryMaster> request);
        Task<bool> AddStateRange(List<StateMaster> request);
        Task<bool> AddCityRange(List<CityMaster> request);        
    }
}
