using Temple.Application.Features.Commands;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class UpdateTempleCommandHandler : IRequestHandler<UpdateTempleCommand, Result>
    {
        private readonly ITempleService _astrologerService;
        private readonly ILoggerService<UpdateTempleCommandHandler> _logger;

        public UpdateTempleCommandHandler(
            ITempleService astrologerService,
            ILoggerService<UpdateTempleCommandHandler> logger)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTempleCommand request, CancellationToken cancellationToken)
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