using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Commands
{
    public class DeleteAstrologerCommandHandler : IRequestHandler<DeleteAstrologerCommand, Result>
    {
        private readonly IAstrologerService _astrologerService;
        private readonly ILoggerService<DeleteAstrologerCommandHandler> _logger;

        public DeleteAstrologerCommandHandler(
            ILoggerService<DeleteAstrologerCommandHandler> logger,
            IAstrologerService astrologerService)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteAstrologerCommand request, CancellationToken cancellationToken)
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