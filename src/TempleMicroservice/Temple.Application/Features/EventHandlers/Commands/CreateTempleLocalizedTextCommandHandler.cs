using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Services;
using Temple.Domain.Entities;

namespace Temple.Application.Features.EventHandlers.Commands
{
    public class CreateTempleLocalizedTextCommandHandler : IRequestHandler<CreateTempleLocalizedTextCommand, Result>
    {
        private readonly ITempleLocalizedTextService _service;
        private readonly ILoggerService<CreateTempleLocalizedTextCommandHandler> _logger;

        public CreateTempleLocalizedTextCommandHandler(ITempleLocalizedTextService service, ILoggerService<CreateTempleLocalizedTextCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateTempleLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Creating new temple localized text");

            try
            {
                var localizedRequest = request.Adapt<TempleLocalizedText>();
                var created = await _service.CreateAsync(localizedRequest);
                if (created)
                    return Result.Success("Temple localized text created successfully.");

                return Result.Failure(new FailureResponse("CREATE_FAILED", "Failed to create temple localized text."));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating temple localized text: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("CREATE_EXCEPTION", "Exception occurred during creation."));
            }
        }
    }

}
