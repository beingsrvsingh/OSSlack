using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleAartiCommandHandler : IRequestHandler<UpdateTempleAartiCommand, Result>
    {
        private readonly ITempleAartiService _service;
        private readonly ILoggerService<UpdateTempleAartiCommandHandler> _logger;

        public UpdateTempleAartiCommandHandler(ITempleAartiService service, ILoggerService<UpdateTempleAartiCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleAartiCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple aarti with ID: {Id}", request.Id);

            try
            {
                var aartiRequest = request.Adapt<TempleAarti>();
                var updated = await _service.UpdateAsync(aartiRequest);
                if (updated)
                    return Result.Success("Temple aarti updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple aarti."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple aarti: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
