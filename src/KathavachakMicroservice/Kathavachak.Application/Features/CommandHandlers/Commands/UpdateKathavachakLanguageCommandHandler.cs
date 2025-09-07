using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakLanguageCommandHandler : IRequestHandler<UpdateKathavachakLanguageCommand, Result>
    {
        private readonly IKathavachakLanguageService _service;
        private readonly ILoggerService<UpdateKathavachakLanguageCommandHandler> _logger;

        public UpdateKathavachakLanguageCommandHandler(
            IKathavachakLanguageService service,
            ILoggerService<UpdateKathavachakLanguageCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null)
                    return Result.Failure(new FailureResponse("NOT_FOUND", "Language entry not found."));

                request.Adapt(existing);

                var result = await _service.UpdateAsync(existing);
                return result
                    ? Result.Success("Language updated successfully.")
                    : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update language."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakLanguageCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
