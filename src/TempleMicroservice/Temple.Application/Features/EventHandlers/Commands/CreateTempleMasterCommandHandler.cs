using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;


namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleMasterCommandHandler : IRequestHandler<CreateTempleMasterCommand, Result>
    {
        private readonly ITempleService _service;
        private readonly ILoggerService<CreateTempleMasterCommandHandler> _logger;

        public CreateTempleMasterCommandHandler(ITempleService service, ILoggerService<CreateTempleMasterCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleMasterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple master");

            try
            {
                var templeRequest = request.Adapt<TempleMaster>();
                var created = await _service.CreateAsync(templeRequest);
                if (created)
                    return Result.Success("Temple master created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple master."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple master: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
