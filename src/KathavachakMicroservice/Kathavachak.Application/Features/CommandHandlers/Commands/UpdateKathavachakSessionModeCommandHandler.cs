using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakSessionModeCommandHandler : IRequestHandler<UpdateKathavachakSessionModeCommand, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<UpdateKathavachakSessionModeCommandHandler> _logger;

        public UpdateKathavachakSessionModeCommandHandler(
            IKathavachakSessionModeService service,
            ILoggerService<UpdateKathavachakSessionModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakSessionModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                if (existing == null)
                {
                    return Result.Failure(new FailureResponse("NOT_FOUND", "Session mode not found."));
                }

                request.Adapt(existing);
                var result = await _service.UpdateAsync(existing);
                return result
                    ? Result.Success("Session mode updated.")
                    : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update session mode."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakSessionModeCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An error occurred while updating."));
            }
        }
    }
}
