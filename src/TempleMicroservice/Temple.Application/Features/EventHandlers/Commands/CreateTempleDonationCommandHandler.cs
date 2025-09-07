using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleDonationCommandHandler : IRequestHandler<CreateTempleDonationCommand, Result>
    {
        private readonly ITempleDonationService _service;
        private readonly ILoggerService<CreateTempleDonationCommandHandler> _logger;

        public CreateTempleDonationCommandHandler(ITempleDonationService service, ILoggerService<CreateTempleDonationCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleDonationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple donation");

            try
            {
                var donationRequest = request.Adapt<TempleDonation>();
                var created = await _service.CreateAsync(donationRequest);
                if (created)
                    return Result.Success("Temple donation created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple donation."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple donation: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
