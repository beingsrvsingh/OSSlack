using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleAartiByIdQueryHandler : IRequestHandler<GetTempleAartiByIdQuery, Result>
    {
        private readonly ITempleAartiService _service;
        private readonly ILoggerService<GetTempleAartiByIdQueryHandler> _logger;

        public GetTempleAartiByIdQueryHandler(ITempleAartiService service, ILoggerService<GetTempleAartiByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleAartiByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple aarti with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple aarti not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("AARTI_NOT_FOUND", "Temple aarti not found."));
        }
    }

}
