using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTemplePrasadCommandHandler : IRequestHandler<DeleteTemplePrasadCommand, Result>
    {
        private readonly ITemplePrasadService _service;
        private readonly ILoggerService<DeleteTemplePrasadCommandHandler> _logger;

        public DeleteTemplePrasadCommandHandler(ITemplePrasadService service, ILoggerService<DeleteTemplePrasadCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTemplePrasadCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple prasad with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple prasad deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple prasad."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple prasad: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
