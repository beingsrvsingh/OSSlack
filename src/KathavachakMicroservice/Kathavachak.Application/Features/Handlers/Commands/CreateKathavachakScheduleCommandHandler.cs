using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class CreateKathavachakScheduleCommandHandler : IRequestHandler<CreateKathavachakScheduleCommand, Result>
    {
        private readonly IKathavachakScheduleService _service;
        private readonly ILoggerService<CreateKathavachakScheduleCommandHandler> _logger;

        public CreateKathavachakScheduleCommandHandler(IKathavachakScheduleService service, ILoggerService<CreateKathavachakScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakSchedule>();
                var success = await _service.CreateAsync(entity);
                return success ? Result.Success("Schedule created.") : Result.Failure(new FailureResponse("CREATE_FAILED", "Unable to create schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakSchedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
