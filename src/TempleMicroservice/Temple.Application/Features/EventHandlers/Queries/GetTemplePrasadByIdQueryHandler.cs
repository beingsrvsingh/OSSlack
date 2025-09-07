using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTemplePrasadByIdQueryHandler : IRequestHandler<GetTemplePrasadByIdQuery, Result>
    {
        private readonly ITemplePrasadService _service;
        private readonly ILoggerService<GetTemplePrasadByIdQueryHandler> _logger;

        public GetTemplePrasadByIdQueryHandler(ITemplePrasadService service, ILoggerService<GetTemplePrasadByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTemplePrasadByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple prasad with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple prasad not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("PRASAD_NOT_FOUND", "Temple prasad not found."));
        }
    }

}
