using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleScheduleCommandHandler : IRequestHandler<CreateTempleScheduleCommand, Result>
    {
        private readonly ITempleScheduleService _service;
        private readonly ILoggerService<CreateTempleScheduleCommandHandler> _logger;

        public CreateTempleScheduleCommandHandler(ITempleScheduleService service, ILoggerService<CreateTempleScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleScheduleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple schedule");

            try
            {
                var scheduleRequest = request.Adapt<TempleSchedule>();
                var created = await _service.CreateAsync(scheduleRequest);
                if (created)
                    return Result.Success("Temple schedule created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple schedule."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple schedule: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
