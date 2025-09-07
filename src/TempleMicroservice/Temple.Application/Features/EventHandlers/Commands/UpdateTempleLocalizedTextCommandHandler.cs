using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleLocalizedTextCommandHandler : IRequestHandler<UpdateTempleLocalizedTextCommand, Result>
    {
        private readonly ITempleLocalizedTextService _service;
        private readonly ILoggerService<UpdateTempleLocalizedTextCommandHandler> _logger;

        public UpdateTempleLocalizedTextCommandHandler(ITempleLocalizedTextService service, ILoggerService<UpdateTempleLocalizedTextCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple localized text with ID: {Id}", request.Id);

            try
            {
                var localizedRequest = request.Adapt<TempleLocalizedText>();
                var updated = await _service.UpdateAsync(localizedRequest);
                if (updated)
                    return Result.Success("Temple localized text updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple localized text."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple localized text: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
