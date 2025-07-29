using Review.Application.Services;
using Review.Domain.Repository;
using Shared.Application.Interfaces.Logging;

namespace Review.Infrastructure.Services
{
    public class SeedService : ISeedService
    {
        private readonly ILoggerService<SeedService> _logger;
        private readonly IReviewReportReasonProvider _reasonProvider;
        private readonly IReviewReportReasonRepository _repository;

        public SeedService(ILoggerService<SeedService> logger, IReviewReportReasonProvider reasonProvider, IReviewReportReasonRepository repository)
        {
            this._logger = logger;
            this._reasonProvider = reasonProvider;
            this._repository = repository;
        }
        public async Task<bool> SeedReviewReportReasonsAsync()
        {
            try
            {
                var existing = await _repository.GetAllAsync();

                if (existing.Any())
                {
                    _logger.LogInfo("Seeding skipped: {Count} ReviewReportReasons already exist.", existing.Count);
                    return true;
                }
                var reasons = _reasonProvider.GetDefaultReasons();

                await _repository.AddRangeAsync(reasons.ToArray());
                await _repository.SaveChangesAsync();

                _logger.LogInfo("Successfully seeded {Count} ReviewReportReasons.", reasons.Count);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to seed ReviewReportReasons. Exception: {Message}", ex.Message);
                return false;
            }
        }

    }
}