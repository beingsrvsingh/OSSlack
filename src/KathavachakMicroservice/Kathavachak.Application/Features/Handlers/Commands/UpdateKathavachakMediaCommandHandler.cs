using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakMediaCommandHandler : IRequestHandler<UpdateKathavachakMediaCommand, Result>
    {
        private readonly IKathavachakMediaService _service;
        private readonly ILoggerService<UpdateKathavachakMediaCommandHandler> _logger;

        public UpdateKathavachakMediaCommandHandler(
            IKathavachakMediaService service,
            ILoggerService<UpdateKathavachakMediaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null)
                    return Result.Failure(new FailureResponse("NOT_FOUND", "Media item not found."));

                request.Adapt(existing);

                var success = await _service.UpdateAsync(existing);
                return success
                    ? Result.Success("Media item updated successfully.")
                    : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update media item."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakMediaCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
