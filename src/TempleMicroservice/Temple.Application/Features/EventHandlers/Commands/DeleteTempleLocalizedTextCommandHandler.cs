using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleLocalizedTextCommandHandler : IRequestHandler<DeleteTempleLocalizedTextCommand, Result>
    {
        private readonly ITempleLocalizedTextService _service;
        private readonly ILoggerService<DeleteTempleLocalizedTextCommandHandler> _logger;

        public DeleteTempleLocalizedTextCommandHandler(ITempleLocalizedTextService service, ILoggerService<DeleteTempleLocalizedTextCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple localized text with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple localized text deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple localized text."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple localized text: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
