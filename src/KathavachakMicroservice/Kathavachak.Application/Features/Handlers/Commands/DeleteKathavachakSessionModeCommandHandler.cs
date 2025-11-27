using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakSessionModeCommandHandler : IRequestHandler<DeleteKathavachakSessionModeCommand, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<DeleteKathavachakSessionModeCommandHandler> _logger;

        public DeleteKathavachakSessionModeCommandHandler(
            IKathavachakSessionModeService service,
            ILoggerService<DeleteKathavachakSessionModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakSessionModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteAsync(request.Id);
                return result
                    ? Result.Success("Session mode deleted.")
                    : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete session mode."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakSessionModeCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An error occurred while deleting."));
            }
        }
    }

}
