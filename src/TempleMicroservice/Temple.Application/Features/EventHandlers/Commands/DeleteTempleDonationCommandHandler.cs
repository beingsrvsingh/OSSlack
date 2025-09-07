using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleDonationCommandHandler : IRequestHandler<DeleteTempleDonationCommand, Result>
    {
        private readonly ITempleDonationService _service;
        private readonly ILoggerService<DeleteTempleDonationCommandHandler> _logger;

        public DeleteTempleDonationCommandHandler(ITempleDonationService service, ILoggerService<DeleteTempleDonationCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleDonationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Deleting temple donation with ID: {Id}", request.Id);

            try
            {
                var deleted = await _service.DeleteAsync(request.Id);
                if (deleted)
                    return Result.Success("Temple donation deleted successfully.");

                return Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete temple donation."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting temple donation: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("DELETE_EXCEPTION", "Exception occurred during deletion."));
            }
        }
    }

}
