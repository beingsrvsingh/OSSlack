using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleDonationCommandHandler : IRequestHandler<UpdateTempleDonationCommand, Result>
    {
        private readonly ITempleDonationService _service;
        private readonly ILoggerService<UpdateTempleDonationCommandHandler> _logger;

        public UpdateTempleDonationCommandHandler(ITempleDonationService service, ILoggerService<UpdateTempleDonationCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleDonationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating temple donation with ID: {Id}", request.Id);

            try
            {
                var templeDonationRequest = request.Adapt<TempleDonation>();
                var updated = await _service.UpdateAsync(templeDonationRequest);
                if (updated)
                    return Result.Success("Temple donation updated successfully.");

                return Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update temple donation."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating temple donation: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("UPDATE_EXCEPTION", "Exception occurred during update."));
            }
        }
    }

}
