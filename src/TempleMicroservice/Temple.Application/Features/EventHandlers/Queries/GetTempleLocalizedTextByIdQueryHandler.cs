using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleLocalizedTextByIdQueryHandler : IRequestHandler<GetTempleLocalizedTextByIdQuery, Result>
    {
        private readonly ITempleLocalizedTextService _service;
        private readonly ILoggerService<GetTempleLocalizedTextByIdQueryHandler> _logger;

        public GetTempleLocalizedTextByIdQueryHandler(ITempleLocalizedTextService service, ILoggerService<GetTempleLocalizedTextByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleLocalizedTextByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching localized text with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Localized text not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("LOCALIZED_TEXT_NOT_FOUND", "Temple localized text not found."));
        }
    }

}
