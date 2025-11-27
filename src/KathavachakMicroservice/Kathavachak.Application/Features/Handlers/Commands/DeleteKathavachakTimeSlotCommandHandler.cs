using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class DeleteKathavachakTimeSlotCommandHandler : IRequestHandler<DeleteKathavachakTimeSlotCommand, Result>
    {
        private readonly IKathavachakTimeSlotService _service;
        private readonly ILoggerService<DeleteKathavachakTimeSlotCommandHandler> _logger;

        public DeleteKathavachakTimeSlotCommandHandler(IKathavachakTimeSlotService service, ILoggerService<DeleteKathavachakTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteKathavachakTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteAsync(request.Id);
                return result ? Result.Success("Time slot deleted.") : Result.Failure(new FailureResponse("DELETE_FAILED", "Failed to delete time slot."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteKathavachakTimeSlot: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
