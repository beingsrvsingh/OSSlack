using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, Result>
    {
        private readonly IScheduleService _service;
        private readonly ILoggerService<CreateScheduleCommandHandler> _logger;

        public CreateScheduleCommandHandler(IScheduleService service, ILoggerService<CreateScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreateScheduleAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create schedule.");
                return Result.Failure("Unable to create schedule.");
            }
        }
    }
}
