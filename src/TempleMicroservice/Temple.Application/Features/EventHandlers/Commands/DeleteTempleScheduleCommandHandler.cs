using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleScheduleCommandHandler : IRequestHandler<DeleteTempleScheduleCommand, Result>
    {
        private readonly ITempleScheduleService _service;
        private readonly ILoggerService<DeleteTempleScheduleCommandHandler> _logger;

        public DeleteTempleScheduleCommandHandler(ITempleScheduleService service, ILoggerService<DeleteTempleScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple schedule with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple schedule deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple schedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
