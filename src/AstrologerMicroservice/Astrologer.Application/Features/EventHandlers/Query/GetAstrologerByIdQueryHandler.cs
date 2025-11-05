
using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Query
{
    public class GetAstrologerByIdQueryHandler : IRequestHandler<GetAstrologerByIdQuery, Result>
    {
        private readonly IAstrologerService _astrologerService;
        private readonly ILoggerService<GetAstrologerByIdQueryHandler> _logger;

        public GetAstrologerByIdQueryHandler(
            IAstrologerService astrologerService,
            ILoggerService<GetAstrologerByIdQueryHandler> logger)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAstrologerByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching astrologer with ID: {Id}", request.Id!);
            var astrologer = await _astrologerService.GetByIdAsync(request.Id);

            if (astrologer != null)
            {
                return Result.Success(astrologer);
            }
            else
            {
                _logger.LogWarning("Astrologer not found for ID: {Id}", request.Id!);
                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "Astrologer not found."));
            }
        }
    }

}