using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetSchedulesByPriestIdQueryHandler : IRequestHandler<GetSchedulesByPriestIdQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetSchedulesByPriestIdQueryHandler> _logger;

        public GetSchedulesByPriestIdQueryHandler(IPriestService priestService, ILoggerService<GetSchedulesByPriestIdQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetSchedulesByPriestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var schedules = await _priestService.GetSchedulesByPriestIdAsync(request.PriestId);
                return Result.Success(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch schedules for priest ID {request.PriestId}");
                return Result.Failure("Error fetching schedules.");
            }
        }
    }

}
