using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleAartiCommandHandler : IRequestHandler<DeleteTempleAartiCommand, Result>
    {
        private readonly ITempleAartiService _service;
        private readonly ILoggerService<DeleteTempleAartiCommandHandler> _logger;

        public DeleteTempleAartiCommandHandler(ITempleAartiService service, ILoggerService<DeleteTempleAartiCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleAartiCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple aarti with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple aarti deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple aarti."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple aarti: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
