using Temple.Application.Features.Commands;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleCommandHandler : IRequestHandler<CreateTempleCommand, Result>
    {
        private readonly ITempleService _astrologerService;
        private readonly ILoggerService<CreateTempleCommandHandler> _logger;

        public CreateTempleCommandHandler(ILoggerService<CreateTempleCommandHandler> logger, ITempleService astrologerService)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleCommand request, CancellationToken cancellationToken)
        {
            var hasCreated = await _astrologerService.CreateAsync(request);
            if (hasCreated)
            {
                return Result.Success(new { Message = "Astrologer created successfully." });
            }
            else
            {
                _logger.LogWarning("Astrologer creation failed for request {@Request}", request);
                return Result.Failure(new FailureResponse("ASTRO_CREATION_FAILED", "Astrologer creation failed"));
            }
        }
    }

}