using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class UpdateKathavachakTimeSlotCommandHandler : IRequestHandler<UpdateKathavachakTimeSlotCommand, Result>
    {
        private readonly IKathavachakTimeSlotService _service;
        private readonly ILoggerService<UpdateKathavachakTimeSlotCommandHandler> _logger;

        public UpdateKathavachakTimeSlotCommandHandler(IKathavachakTimeSlotService service, ILoggerService<UpdateKathavachakTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateKathavachakTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _service.GetByIdAsync(request.Id);
                if (entity == null) return Result.Failure(new FailureResponse("NOT_FOUND", "Time slot not found."));

                request.Adapt(entity);
                var result = await _service.UpdateAsync(entity);
                return result ? Result.Success("Time slot updated.") : Result.Failure(new FailureResponse("UPDATE_FAILED", "Failed to update time slot."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateKathavachakTimeSlot: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
