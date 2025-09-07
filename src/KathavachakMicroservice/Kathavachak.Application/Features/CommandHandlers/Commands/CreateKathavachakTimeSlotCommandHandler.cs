using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class CreateKathavachakTimeSlotCommandHandler : IRequestHandler<CreateKathavachakTimeSlotCommand, Result>
    {
        private readonly IKathavachakTimeSlotService _service;
        private readonly ILoggerService<CreateKathavachakTimeSlotCommandHandler> _logger;

        public CreateKathavachakTimeSlotCommandHandler(IKathavachakTimeSlotService service, ILoggerService<CreateKathavachakTimeSlotCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakTimeSlot>();
                var result = await _service.CreateAsync(entity);
                return result ? Result.Success("Time slot created.") : Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create time slot."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakTimeSlot: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", ex.Message));
            }
        }
    }
}
