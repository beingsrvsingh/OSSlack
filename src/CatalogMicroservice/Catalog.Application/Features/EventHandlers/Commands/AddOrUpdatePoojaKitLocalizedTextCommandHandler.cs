using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class AddOrUpdatePoojaKitLocalizedTextCommandHandler : IRequestHandler<AddOrUpdatePoojaKitLocalizedTextCommand, Result>
    {
        private readonly ILoggerService<AddOrUpdatePoojaKitLocalizedTextCommandHandler> _logger;
        private readonly IPoojaKitService _service;

        public AddOrUpdatePoojaKitLocalizedTextCommandHandler(ILoggerService<AddOrUpdatePoojaKitLocalizedTextCommandHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(AddOrUpdatePoojaKitLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.AddOrUpdateLocalizedTextAsync(request.LocalizedText);
            if (!success)
            {
                return Result.Failure(new FailureResponse("Error", "Failed to add or update localized text."));
            }
            return Result.Success(true);
        }
    }
}