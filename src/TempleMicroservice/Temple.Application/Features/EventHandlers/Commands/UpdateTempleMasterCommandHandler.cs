using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleMasterCommandHandler : IRequestHandler<UpdateTempleMasterCommand, Result>
    {
        private readonly ITempleService _service;
        private readonly ILoggerService<UpdateTempleMasterCommandHandler> _logger;

        public UpdateTempleMasterCommandHandler(ITempleService service, ILoggerService<UpdateTempleMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple master with ID: {Id}", request.Id);

            try
            {
                var templeRequest = request.Adapt<TempleMaster>();
                var updated = await _service.UpdateAsync(templeRequest);
                if (updated)
                    return Result.Success("Temple master updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple master."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple master: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
