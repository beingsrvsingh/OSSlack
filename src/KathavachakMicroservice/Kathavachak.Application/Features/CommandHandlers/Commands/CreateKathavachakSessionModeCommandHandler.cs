using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class CreateKathavachakSessionModeCommandHandler : IRequestHandler<CreateKathavachakSessionModeCommand, Result>
    {
        private readonly IKathavachakSessionModeService _service;
        private readonly ILoggerService<CreateKathavachakSessionModeCommandHandler> _logger;

        public CreateKathavachakSessionModeCommandHandler(
            IKathavachakSessionModeService service,
            ILoggerService<CreateKathavachakSessionModeCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakSessionModeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakSessionMode>();
                var result = await _service.CreateAsync(entity);
                return result
                    ? Result.Success("Session mode created.")
                    : Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create session mode."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakSessionModeCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An error occurred while creating."));
            }
        }
    }

}
