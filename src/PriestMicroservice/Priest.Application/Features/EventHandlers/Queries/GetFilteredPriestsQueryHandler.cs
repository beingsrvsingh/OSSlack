using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Priest.Domain.enums;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{

    public class GetFilteredPriestsQueryHandler : IRequestHandler<GetFilteredPriestsQuery, Result>
    {
        private readonly ILoggerService<GetFilteredPriestsQueryHandler> _logger;
        private readonly IPriestService _priestService;

        public GetFilteredPriestsQueryHandler(
            ILoggerService<GetFilteredPriestsQueryHandler> logger,
            IPriestService priestService)
        {
            _logger = logger;
            _priestService = priestService;
        }

        public async Task<Result> Handle(GetFilteredPriestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allActivePriests = await _priestService.GetAllActivePriestsAsync();

                var filteredPriests = allActivePriests;

                if (!string.IsNullOrWhiteSpace(request.Language))
                {
                    filteredPriests = filteredPriests.Where(p =>
                        p.PriestLanguages.Any(l => l.Language.Equals(request.Language, StringComparison.OrdinalIgnoreCase))
                    );
                }

                if (!string.IsNullOrWhiteSpace(request.Expertise) && Enum.TryParse<PriestExpertiseType>(request.Expertise, true, out var expertiseEnum))
                {
                    filteredPriests = filteredPriests.Where(p => p.PriestExpertise.Any(e => e.ExpertiseArea == expertiseEnum));
                }

                return Result.Success(filteredPriests.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch filtered priests.");
                return Result.Failure("An error occurred while filtering priests.");
            }
        }
    }

}
