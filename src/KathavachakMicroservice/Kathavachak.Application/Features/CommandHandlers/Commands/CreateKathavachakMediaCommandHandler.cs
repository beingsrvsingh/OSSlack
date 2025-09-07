using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Services;
using Kathavachak.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.CommandHandlers.Commands
{
    public class CreateKathavachakMediaCommandHandler : IRequestHandler<CreateKathavachakMediaCommand, Result>
    {
        private readonly IKathavachakMediaService _service;
        private readonly ILoggerService<CreateKathavachakMediaCommandHandler> _logger;

        public CreateKathavachakMediaCommandHandler(
            IKathavachakMediaService service,
            ILoggerService<CreateKathavachakMediaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateKathavachakMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = request.Adapt<KathavachakMedia>();

                var success = await _service.CreateAsync(entity);
                return success
                    ? Result.Success("Media item created successfully.")
                    : Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create media item."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateKathavachakMediaCommand: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("EXCEPTION", "An unexpected error occurred."));
            }
        }
    }

}
