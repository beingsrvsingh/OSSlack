using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakScheduleCommandHandler : IRequestHandler<UpdateKathavachakScheduleCommand, Result>
    {
        private readonly IKathavachakScheduleService _service;
        private readonly ILoggerService<UpdateKathavachakScheduleCommandHandler> _logger;

        public UpdateKathavachakScheduleCommandHandler(IKathavachakScheduleService service, ILoggerService<UpdateKathavachakScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null) return Result.Failure(new FailureResponse("NOT_FOUND", "Schedule not found"));

                request.Adapt(existing);
                var success = await _service.UpdateAsync(existing);
                return success ? Result.Success("Schedule updated.") : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakSchedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
