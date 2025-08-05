
using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Query
{
    public class GetAstrologerByIdQueryHandler : IRequestHandler<GetSearchAstrologersQuery, Result>
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

        public async Task<Result> Handle(GetSearchAstrologersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching astrologer with ID: {Id}", request.Language!);
            var astrologer = await _astrologerService.GetAllAsync();

            if (astrologer != null)
            {
                return Result.Success(astrologer);
            }
            else
            {
                _logger.LogWarning("Astrologer not found for ID: {Id}", request.Language!);
                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "Astrologer not found."));
            }
        }
    }

}