using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleExceptionByIdQueryHandler : IRequestHandler<GetTempleExceptionByIdQuery, Result>
    {
        private readonly ITempleExceptionService _service;
        private readonly ILoggerService<GetTempleExceptionByIdQueryHandler> _logger;

        public GetTempleExceptionByIdQueryHandler(ITempleExceptionService service, ILoggerService<GetTempleExceptionByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleExceptionByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple exception with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple exception not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("EXCEPTION_NOT_FOUND", "Temple exception not found."));
        }
    }

}
