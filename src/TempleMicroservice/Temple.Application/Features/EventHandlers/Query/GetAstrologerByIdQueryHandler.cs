
using Temple.Application.Features.Query;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Query
{
    public class GetAstrologerByIdQueryHandler : IRequestHandler<GetTempleByIdQuery, Result>
    {
        private readonly ITempleService _templeService;
        private readonly ILoggerService<GetAstrologerByIdQueryHandler> _logger;

        public GetAstrologerByIdQueryHandler(
            ITempleService astrologerService,
            ILoggerService<GetAstrologerByIdQueryHandler> logger)
        {
            _templeService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple with ID: {Id}", request.Id);
            var temple = await _templeService.GetByIdAsync(request.Id);

            if (temple != null)
            {
                return Result.Success(temple);
            }
            else
            {
                _logger.LogWarning("Temple not found for ID: {Id}", request.Id!);
                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "Astrologer not found."));
            }
        }
    }

}