using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleExceptionCommandHandler : IRequestHandler<DeleteTempleExceptionCommand, Result>
    {
        private readonly ITempleExceptionService _service;
        private readonly ILoggerService<DeleteTempleExceptionCommandHandler> _logger;

        public DeleteTempleExceptionCommandHandler(ITempleExceptionService service, ILoggerService<DeleteTempleExceptionCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleExceptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple exception with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple exception deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple exception."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple exception: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
