using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleAartiCommandHandler : IRequestHandler<CreateTempleAartiCommand, Result>
    {
        private readonly ITempleAartiService _service;
        private readonly ILoggerService<CreateTempleAartiCommandHandler> _logger;

        public CreateTempleAartiCommandHandler(ITempleAartiService service, ILoggerService<CreateTempleAartiCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleAartiCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple aarti");

            try
            {
                var templeArtiRequest = request.Adapt<TempleAarti>();
                var created = await _service.CreateAsync(templeArtiRequest);
                if (created)
                    return Result.Success("Temple aarti created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple aarti."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple aarti: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
