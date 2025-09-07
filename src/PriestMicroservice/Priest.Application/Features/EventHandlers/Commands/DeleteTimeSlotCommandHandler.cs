using MediatR;
using Priest.Application.Features.Commands;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Commands
{
    public class DeleteTimeSlotCommandHandler : IRequestHandler<DeleteTimeSlotCommand, Result>
    {
        private readonly ITimeSlotService _service;
        private readonly ILoggerService<DeleteTimeSlotCommandHandler> _logger;

        public DeleteTimeSlotCommandHandler(ITimeSlotService service, ILoggerService<DeleteTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _service.DeleteTimeSlotAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete time slot.");
                return Result.Failure("Unable to delete time slot.");
            }
        }
    }
}
