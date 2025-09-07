using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakScheduleCommandHandler : IRequestHandler<DeleteKathavachakScheduleCommand, Result>
    {
        private readonly IKathavachakScheduleService _service;
        private readonly ILoggerService<DeleteKathavachakScheduleCommandHandler> _logger;

        public DeleteKathavachakScheduleCommandHandler(IKathavachakScheduleService service, ILoggerService<DeleteKathavachakScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _service.DeleteAsync(request.Id);
                return success ? Result.Success("Schedule deleted.") : Result.Failure(new FailureResponse("DELETE_FAILED", "Unable to delete schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakSchedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
