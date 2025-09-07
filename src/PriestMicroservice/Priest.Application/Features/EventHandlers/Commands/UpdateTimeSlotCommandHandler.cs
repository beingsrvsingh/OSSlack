using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class UpdateTimeSlotCommandHandler : IRequestHandler<UpdateTimeSlotCommand, Result>
    {
        private readonly ITimeSlotService _service;
        private readonly ILoggerService<UpdateTimeSlotCommandHandler> _logger;

        public UpdateTimeSlotCommandHandler(ITimeSlotService service, ILoggerService<UpdateTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.UpdateTimeSlotAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update time slot.");
                return Result.Failure("Unable to update time slot.");
            }
        }
    }
}
