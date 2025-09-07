using Kathavachak.Application.Contracts;
using Kathavachak.Application.Features.Queries;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Queries
{
    public class GetAllKathavachakMastersQueryHandler : IRequestHandler<GetAllKathavachakMastersQuery, Result>
    {
        private readonly IKathavachakTimeSlotService _service;
        private readonly ILoggerService<GetAllKathavachakMastersQueryHandler> _logger;

        public GetAllKathavachakMastersQueryHandler(
            IKathavachakTimeSlotService service,
            ILoggerService<GetAllKathavachakMastersQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAllKathavachakMastersQuery request, CancellationToken cancellationToken)
        {
            var timeSlots = await _service.GetAllAsync();

            if (timeSlots == null || !timeSlots.Any())
            {
                _logger.LogWarning("No Kathavachak time slots found.");
                return Result.Failure(new FailureResponse("NOT_FOUND", "No Kathavachak time slots found."));
            }

            var dtos = timeSlots.Adapt<List<KathavachakTimeSlotDto>>();
            return Result.Success(dtos);
        }
    }
}
