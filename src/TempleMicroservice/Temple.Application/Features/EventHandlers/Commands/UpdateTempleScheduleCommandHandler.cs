using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleScheduleCommandHandler : IRequestHandler<UpdateTempleScheduleCommand, Result>
    {
        private readonly ITempleScheduleService _service;
        private readonly ILoggerService<UpdateTempleScheduleCommandHandler> _logger;

        public UpdateTempleScheduleCommandHandler(ITempleScheduleService service, ILoggerService<UpdateTempleScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple schedule with ID: {Id}", request.Id);

            try
            {
                var templeScheduleRequest = request.Adapt<TempleSchedule>();
                var updated = await _service.UpdateAsync(templeScheduleRequest);
                if (updated)
                    return Result.Success("Temple schedule updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple schedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
