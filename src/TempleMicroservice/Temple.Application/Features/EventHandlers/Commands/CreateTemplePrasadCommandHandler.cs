using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTemplePrasadCommandHandler : IRequestHandler<CreateTemplePrasadCommand, Result>
    {
        private readonly ITemplePrasadService _service;
        private readonly ILoggerService<CreateTemplePrasadCommandHandler> _logger;

        public CreateTemplePrasadCommandHandler(ITemplePrasadService service, ILoggerService<CreateTemplePrasadCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTemplePrasadCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple prasad");

            try
            {
                var prasadRequest = request.Adapt<TemplePrasad>();
                var created = await _service.CreateAsync(prasadRequest);
                if (created)
                    return Result.Success("Temple prasad created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple prasad."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple prasad: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
