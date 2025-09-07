using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleScheduleByIdQueryHandler : IRequestHandler<GetTempleScheduleByIdQuery, Result>
    {
        private readonly ITempleScheduleService _service;
        private readonly ILoggerService<GetTempleScheduleByIdQueryHandler> _logger;

        public GetTempleScheduleByIdQueryHandler(ITempleScheduleService service, ILoggerService<GetTempleScheduleByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple schedule with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple schedule not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("SCHEDULE_NOT_FOUND", "Temple schedule not found."));
        }
    }

}
