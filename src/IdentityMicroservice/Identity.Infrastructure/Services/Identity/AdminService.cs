using System.Linq.Expressions;
using Identity.Application.Contracts;
using Identity.Application.Features.Admin.Commands;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Mapster;
using Shared.Application.Interfaces.Logging;
using System.Linq.Dynamic.Core;


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

        public async Task<(List<CountryResponse> Items, int TotalCount)> GetCountriesAsync(
            string? searchTerm = null,
            string? sortBy = "Name",
            bool isDescending = false,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            // Build filter expression: filter by searchTerm if provided
            Expression<Func<CountryMaster, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filter = c => c.Name.Contains(searchTerm);
            }

            // Build orderBy function dynamically using System.Linq.Dynamic.Core
            Func<IQueryable<CountryMaster>, IOrderedQueryable<CountryMaster>>? orderBy = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                string sorting = isDescending ? $"{sortBy} descending" : sortBy;
                orderBy = q => q.OrderBy(sorting);
            }

            var (countries, totalCount) = await repository.CountryMasterRepository.GetPaginatedAsync(
                filter,
                orderBy,
                pageNumber,
                pageSize,
                cancellationToken);
            
            var result = countries.Adapt<List<CountryResponse>>();

            return (result, totalCount);
        }

        public async Task<bool> AddCountryAsync(CountryCommand request)
        {
            try
            {
                var country = request.Adapt<CountryMaster>();

                repository.CountryMasterRepository.AddAsync(country);
                await repository.CountryMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add country");
                return false;
            }
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

        public async Task<bool> AddStateAsync(StateCommand request)
        {
            try
            {
                var state = request.Adapt<StateMaster>();
                
                repository.StateMasterRepository.AddAsync(state);
                await repository.StateMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add state");
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

        public async Task<bool> AddCityAsync(CityCommand request)
        {
            try
            {
                var city = request.Adapt<CityMaster>();

                repository.CityMasterRepository.AddAsync(city);
                await repository.CityMasterRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add city");
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

        public async Task<StateMaster?> GetStateByName(string name)
        {
            return await repository.StateMasterRepository.GetBy(state => state.Name.Contains(name));
        }

        public async Task<CountryMaster?> GetCountryByNameAsync(string countryName)
        {
            return await repository.CountryMasterRepository.GetBy(count => count.Name.Contains(countryName));
        }

        public async Task<bool> BulkInsertCountriesAsync(IEnumerable<CountryMaster> countries, int? batchSize = null, CancellationToken cancellationToken = default)
        {
            try
            {
                await repository.CountryMasterRepository.BulkInsertAsync(countries.ToList(), batchSize, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bulk insert of countries failed.");
                return false;
            }
        }

        public async Task<bool> BulkInsertStatesAsync(IEnumerable<StateMaster> states, int? batchSize = null, CancellationToken cancellationToken = default)
        {
            try
            {
                await repository.StateMasterRepository.BulkInsertAsync(states.ToList(), batchSize, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bulk insert of countries failed.");
                return false;
            }
        }

        public async Task<bool> BulkInsertCitiesAsync(IEnumerable<CityMaster> cities, int? batchSize = null, CancellationToken cancellationToken = default)
        {
            try
            {
                await repository.CityMasterRepository.BulkInsertAsync(cities.ToList(), batchSize, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bulk insert of countries failed.");
                return false;
            }
        }
    }
}
