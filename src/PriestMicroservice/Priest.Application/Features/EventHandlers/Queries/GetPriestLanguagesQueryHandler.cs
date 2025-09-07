using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetPriestLanguagesQueryHandler : IRequestHandler<GetPriestLanguagesQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetPriestLanguagesQueryHandler> _logger;

        public GetPriestLanguagesQueryHandler(IPriestService priestService, ILoggerService<GetPriestLanguagesQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetPriestLanguagesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var languages = await _priestService.GetLanguagesByPriestIdAsync(request.PriestId);
                return Result.Success(languages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch languages for priest ID {request.PriestId}");
                return Result.Failure("Error fetching languages.");
            }
        }
    }

}
