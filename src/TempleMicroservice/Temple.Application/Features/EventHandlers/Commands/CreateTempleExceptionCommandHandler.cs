using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleExceptionCommandHandler : IRequestHandler<CreateTempleExceptionCommand, Result>
    {
        private readonly ITempleExceptionService _service;
        private readonly ILoggerService<CreateTempleExceptionCommandHandler> _logger;

        public CreateTempleExceptionCommandHandler(ITempleExceptionService service, ILoggerService<CreateTempleExceptionCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleExceptionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple exception");

            try
            {
                var exceptionRequest = request.Adapt<TempleException>();
                var created = await _service.CreateAsync(exceptionRequest);
                if (created)
                    return Result.Success("Temple exception created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple exception."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple exception: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
