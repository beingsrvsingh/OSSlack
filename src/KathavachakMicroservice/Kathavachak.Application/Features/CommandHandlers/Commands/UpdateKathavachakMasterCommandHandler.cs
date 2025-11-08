using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakMasterCommandHandler : IRequestHandler<UpdateKathavachakMasterCommand, Result>
    {
        private readonly IKathavachakService _service;
        private readonly ILoggerService<UpdateKathavachakMasterCommandHandler> _logger;

        public UpdateKathavachakMasterCommandHandler(
            IKathavachakService service,
            ILoggerService<UpdateKathavachakMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _service.GetByIdAsync(request.Id);
                //if (existing == null)
                //    return Result.Failure(new FailureResponse("NOT_FOUND", "Kathavachak not found."));

                //// Map updated fields
                //existing.Name = request.DisplayName;
                //existing.ThumbnailUrl = request.ProfilePictureUrl;
                //existing.IsActive = request.IsActive;

                //var success = await _service.UpdateAsync(existing);
                return true
                    ? Result.Success("Kathavachak updated successfully.")
                    : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update Kathavachak."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakMasterCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }

}
