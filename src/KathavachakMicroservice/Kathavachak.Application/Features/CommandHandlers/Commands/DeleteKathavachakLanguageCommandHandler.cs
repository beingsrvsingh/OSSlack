using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakLanguageCommandHandler : IRequestHandler<DeleteKathavachakLanguageCommand, Result>
    {
        private readonly IKathavachakLanguageService _service;
        private readonly ILoggerService<DeleteKathavachakLanguageCommandHandler> _logger;

        public DeleteKathavachakLanguageCommandHandler(
            IKathavachakLanguageService service,
            ILoggerService<DeleteKathavachakLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _service.DeleteAsync(request.Id);
                return success
                    ? Result.Success("Language removed successfully.")
                    : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to remove language."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakLanguageCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
