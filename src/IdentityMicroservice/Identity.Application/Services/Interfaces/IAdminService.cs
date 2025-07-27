using Identity.Application.Contracts;
using Identity.Application.Features.Admin.Commands;
using Identity.Domain.Entities;
using Shared.Domain.Contracts;

namespace Identity.Application.Services.Interfaces
{
    public interface IAdminService
    {
        Task<CountryMaster?> GetCountryByNameAsync(string countryName);
        Task<StateMaster?> GetStateByName(string name);
        Task<bool> AddCountryAsync(CountryCommand request);
        Task<bool> AddCountryRange(List<CountryMaster> request);
        Task<bool> AddStateAsync(StateCommand request);
        Task<bool> AddStateRange(List<StateMaster> request);
        Task<bool> AddCityAsync(CityCommand request);
        Task<bool> AddCityRange(List<CityMaster> request);
        Task<bool> BulkInsertCountriesAsync(IEnumerable<CountryMaster> countries, int? batchSize = null, CancellationToken cancellationToken = default);
        Task<bool> BulkInsertStatesAsync(IEnumerable<StateMaster> states, int? batchSize = null, CancellationToken cancellationToken = default);
        Task<bool> BulkInsertCitiesAsync(IEnumerable<CityMaster> cities, int? batchSize = null, CancellationToken cancellationToken = default);
        Task<(List<CountryResponse> Items, int TotalCount)> GetCountriesAsync(
        string? searchTerm = null,
        string? sortBy = "Name",
        bool isDescending = false,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    }
}
