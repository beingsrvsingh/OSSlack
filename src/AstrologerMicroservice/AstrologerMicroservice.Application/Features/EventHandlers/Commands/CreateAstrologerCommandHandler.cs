using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Commands
{
    public class CreateAstrologerCommandHandler : IRequestHandler<CreateAstrologerCommand, Result>
    {
        private readonly IAstrologerService _astrologerService;
        private readonly ILoggerService<CreateAstrologerCommandHandler> _logger;

        public CreateAstrologerCommandHandler(ILoggerService<CreateAstrologerCommandHandler> logger, IAstrologerService astrologerService)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateAstrologerCommand request, CancellationToken cancellationToken)
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