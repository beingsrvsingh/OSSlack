using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetRitualServicePackagesByPriestIdQueryHandler : IRequestHandler<GetRitualServicePackagesByPriestIdQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetRitualServicePackagesByPriestIdQueryHandler> _logger;

        public GetRitualServicePackagesByPriestIdQueryHandler(IPriestService priestService, ILoggerService<GetRitualServicePackagesByPriestIdQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetRitualServicePackagesByPriestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var packages = await _priestService.GetRitualPackagesByPriestIdAsync(request.PriestId);
                return Result.Success(packages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch ritual service packages for priest ID {request.PriestId}");
                return Result.Failure("Error fetching ritual packages.");
            }
        }
    }

}
