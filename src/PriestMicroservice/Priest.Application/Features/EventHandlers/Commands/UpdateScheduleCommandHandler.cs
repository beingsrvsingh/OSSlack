using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, Result>
    {
        private readonly IScheduleService _service;
        private readonly ILoggerService<UpdateScheduleCommandHandler> _logger;

        public UpdateScheduleCommandHandler(IScheduleService service, ILoggerService<UpdateScheduleCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdateScheduleAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update schedule.");
                return Result.Failure("Unable to update schedule.");
            }
        }
    }

}
