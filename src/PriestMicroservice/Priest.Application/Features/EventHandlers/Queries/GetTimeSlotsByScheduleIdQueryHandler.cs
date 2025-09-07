using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetTimeSlotsByScheduleIdQueryHandler : IRequestHandler<GetTimeSlotsByScheduleIdQuery, Result>
    {
        private readonly IPriestService _priestService;
        private readonly ILoggerService<GetTimeSlotsByScheduleIdQueryHandler> _logger;

        public GetTimeSlotsByScheduleIdQueryHandler(IPriestService priestService, ILoggerService<GetTimeSlotsByScheduleIdQueryHandler> logger)
        {
            _priestService = priestService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTimeSlotsByScheduleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var timeSlots = await _priestService.GetTimeSlotsByScheduleIdAsync(request.ScheduleId);
                return Result.Success(timeSlots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch time slots for schedule ID {request.ScheduleId}");
                return Result.Failure("Error fetching time slots.");
            }
        }
    }

}
