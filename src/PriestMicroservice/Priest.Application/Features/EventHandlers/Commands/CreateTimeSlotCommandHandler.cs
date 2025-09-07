using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class CreateTimeSlotCommandHandler : IRequestHandler<CreateTimeSlotCommand, Result>
    {
        private readonly ITimeSlotService _service;
        private readonly ILoggerService<CreateTimeSlotCommandHandler> _logger;

        public CreateTimeSlotCommandHandler(ITimeSlotService service, ILoggerService<CreateTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.CreateTimeSlotAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create time slot.");
                return Result.Failure("Unable to create time slot.");
            }
        }
    }

}
