using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Infrastructure.Services
{
    public class ServicePackageService : IServicePackageService
    {
        private readonly IServicePackageRepository _repository;
        private readonly ILoggerService<ServicePackageService> _logger;

        public ServicePackageService(IServicePackageRepository repository, ILoggerService<ServicePackageService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> CreateServicePackageAsync(CreateRitualServicePackageCommand command)
        {
            try
            {
                var package = new ServicePackage
                {
                    PriestId = command.PriestId,
                    Name = command.Title,
                    Description = command.Description,
                    Price = command.Price
                };

                await _repository.AddAsync(package);
                return Result.Success("Ritual service package created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create ritual service package.");
                return Result.Failure("Unable to create ritual service package.");
            }
        }

        public async Task<Result> UpdateServicePackageAsync(UpdateRitualServicePackageCommand command)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(command.Id);
                if (existing == null)
                    return Result.Failure("Package not found.");

                existing.Name = command.Title;
                existing.Description = command.Description;
                existing.Price = command.Price;

                await _repository.UpdateAsync(existing);
                return Result.Success("Ritual service package updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update ritual service package.");
                return Result.Failure("Unable to update ritual service package.");
            }
        }

        public async Task<Result> DeleteServicePackageAsync(int id)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                    return Result.Failure("Package not found.");

                await _repository.DeleteAsync(existing);
                return Result.Success("Ritual service package deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete ritual service package.");
                return Result.Failure("Unable to delete ritual service package.");
            }
        }
    }

}
