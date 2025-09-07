using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleByIdQueryHandler : IRequestHandler<GetTempleByIdQuery, Result>
    {
        private readonly ITempleService _templeService;
        private readonly ILoggerService<GetTempleByIdQueryHandler> _logger;

        public GetTempleByIdQueryHandler(
            ITempleService templeService,
            ILoggerService<GetTempleByIdQueryHandler> logger)
        {
            _templeService = templeService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple with ID: {Id}", request.Id);

            var temple = await _templeService.GetByIdWithDetailsAsync(request.Id);

            if (temple != null)
            {
                return Result.Success(temple);
            }
            else
            {
                _logger.LogWarning("Temple not found for ID: {Id}", request.Id);
                return Result.Failure(new FailureResponse("TEMPLE_NOT_FOUND", "Temple not found."));
            }
        }
    }

}
