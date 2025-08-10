using Temple.Application.Features.Commands;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class DeleteTempleCommandHandler : IRequestHandler<DeleteTempleCommand, Result>
    {
        private readonly ITempleService _astrologerService;
        private readonly ILoggerService<DeleteTempleCommandHandler> _logger;

        public DeleteTempleCommandHandler(
            ILoggerService<DeleteTempleCommandHandler> logger,
            ITempleService astrologerService)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTempleCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _astrologerService.DeleteAsync(request.Id);
            if (deleted)
            {
                return Result.Success("Astrologer deleted successfully.");
            }
            else
            {
                _logger.LogWarning("Astrologer deletion failed for ID: {Id}", request.Id);
                return Result.Failure(new FailureResponse("ASTRO_DELETE_FAILED", "Astrologer deletion failed."));
            }
        }
    }

}