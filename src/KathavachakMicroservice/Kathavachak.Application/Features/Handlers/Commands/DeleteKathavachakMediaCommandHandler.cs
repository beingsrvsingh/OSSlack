using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakMediaCommandHandler : IRequestHandler<DeleteKathavachakMediaCommand, Result>
    {
        private readonly IKathavachakMediaService _service;
        private readonly ILoggerService<DeleteKathavachakMediaCommandHandler> _logger;

        public DeleteKathavachakMediaCommandHandler(
            IKathavachakMediaService service,
            ILoggerService<DeleteKathavachakMediaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _service.DeleteAsync(request.Id);
                return success
                    ? Result.Success("Media item deleted successfully.")
                    : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete media item."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakMediaCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
