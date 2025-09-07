using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleExceptionCommandHandler : IRequestHandler<UpdateTempleExceptionCommand, Result>
    {
        private readonly ITempleExceptionService _service;
        private readonly ILoggerService<UpdateTempleExceptionCommandHandler> _logger;

        public UpdateTempleExceptionCommandHandler(ITempleExceptionService service, ILoggerService<UpdateTempleExceptionCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleExceptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple exception with ID: {Id}", request.Id);

            try
            {
                var templeExceptionRequest = request.Adapt<TempleException>();
                var updated = await _service.UpdateAsync(templeExceptionRequest);
                if (updated)
                    return Result.Success("Temple exception updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple exception."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple exception: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
