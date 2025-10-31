using Pooja.Application.Services;
using Pooja.Domain.Core.Repository;
using Pooja.Domain.Entities;
using Shared.Application.Interfaces.Logging;


namespace Pooja.Infrastructure.Services
{
    public class PoojaService : IPoojaService
    {
        private readonly IPoojaMasterRepository _repository;
        private readonly ILoggerService<PoojaService> _logger;

        public PoojaService(IPoojaMasterRepository repository, ILoggerService<PoojaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PoojaMaster>> GetAllPoojasAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve poojas.");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<PoojaMaster?> GetPoojaByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve pooja with id: {id}");
                return null;
            }
        }

        public async Task<IEnumerable<PoojaMaster>> GetPoojasByTempleAsync(int templeId)
        {
            try
            {
                return await _repository.GetByTempleAsync(templeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve poojas for temple id: {templeId}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<IEnumerable<PoojaMaster>> GetPoojasByPriestAsync(int priestId)
        {
            try
            {
                return await _repository.GetByPriestAsync(priestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve poojas for priest id: {priestId}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task AddPoojaAsync(PoojaMaster pooja)
        {
            try
            {
                await _repository.AddAsync(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add pooja.");
                throw;
            }
        }

        public async Task UpdatePoojaAsync(PoojaMaster pooja)
        {
            try
            {
                await _repository.UpdateAsync(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update pooja with id: {pooja.Id}");
                throw;
            }
        }

        public async Task DeletePoojaAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete pooja with id: {id}");
                throw;
            }
        }

        public async Task<IEnumerable<PoojaMaster>> SearchPoojasAsync(string keyword)
        {
            try
            {
                var all = await _repository.GetAllAsync();
                return all.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to search poojas with keyword: {keyword}");
                return Enumerable.Empty<PoojaMaster>();
            }
        }

        public async Task<bool> IsPoojaAvailableAtHomeAsync(int poojaId)
        {
            try
            {
                var pooja = await _repository.GetByIdAsync(poojaId);
                return pooja?.IsHomeAvailable ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to check if pooja id {poojaId} is available at home.");
                return false;
            }
        }
    }
}