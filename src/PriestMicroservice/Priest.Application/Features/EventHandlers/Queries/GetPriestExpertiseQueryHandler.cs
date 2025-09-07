using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetPriestExpertiseQueryHandler : IRequestHandler<GetPriestLanguagesQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetPriestExpertiseQueryHandler> _logger;

        public GetPriestExpertiseQueryHandler(IPriestService priestService, ILoggerService<GetPriestExpertiseQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPriestLanguagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var expertises = await _priestService.GetExpertiseByPriestIdAsync(request.PriestId);
                return Result.Success(expertises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch expertises for priest ID {request.PriestId}");
                return Result.Failure("Error fetching expertises.");
            }
        }
    }

}
