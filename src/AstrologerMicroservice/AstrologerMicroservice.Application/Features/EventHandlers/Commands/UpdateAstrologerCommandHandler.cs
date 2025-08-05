using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Commands
{
    public class UpdateAstrologerCommandHandler : IRequestHandler<UpdateAstrologerCommand, Result>
    {
        private readonly IAstrologerService _astrologerService;
        private readonly ILoggerService<UpdateAstrologerCommandHandler> _logger;

        public UpdateAstrologerCommandHandler(
            IAstrologerService astrologerService,
            ILoggerService<UpdateAstrologerCommandHandler> logger)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateAstrologerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Updating astrologer with ID: {Id}", request.Id);

            bool updated = await _astrologerService.UpdateAsync(request);

            if (updated)
            {
                return Result.Success("Astrologer updated successfully.");
            }
            else
            {
                _logger.LogWarning("Astrologer update failed for ID: {Id}", request.Id);
                return Result.Failure(new FailureResponse("ASTRO_UPDATE_FAILED", "Astrologer update failed."));
            }
        }
    }

}