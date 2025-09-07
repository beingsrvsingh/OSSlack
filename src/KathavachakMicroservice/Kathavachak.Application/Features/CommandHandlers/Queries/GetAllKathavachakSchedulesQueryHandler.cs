using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetAllKathavachakSchedulesQueryHandler : IRequestHandler<GetAllKathavachakSchedulesQuery, Result>
    {
        private readonly IKathavachakScheduleService _service;
        private readonly ILoggerService<GetAllKathavachakSchedulesQueryHandler> _logger;

        public GetAllKathavachakSchedulesQueryHandler(
            IKathavachakScheduleService service,
            ILoggerService<GetAllKathavachakSchedulesQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAllKathavachakSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _service.GetAllAsync();

            if (schedules == null || !schedules.Any())
            {
                _logger.LogWarning("No Kathavachak schedules found.");
                return Result.Failure(new FailureResponse("NOT_FOUND", "No Kathavachak schedules found."));
            }

            var dtos = schedules.Adapt<List<KathavachakScheduleDto>>();
            return Result.Success(dtos);
        }
    }
}
