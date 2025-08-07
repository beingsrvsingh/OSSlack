using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class AddOrUpdatePoojaKitItemLocalizedTextCommandHandler : IRequestHandler<AddOrUpdatePoojaKitItemLocalizedTextCommand, Result>
    {
        private readonly ILoggerService<AddOrUpdatePoojaKitItemLocalizedTextCommandHandler> _logger;
        private readonly IPoojaKitItemService _service;

        public AddOrUpdatePoojaKitItemLocalizedTextCommandHandler(ILoggerService<AddOrUpdatePoojaKitItemLocalizedTextCommandHandler> logger, IPoojaKitItemService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(AddOrUpdatePoojaKitItemLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.AddOrUpdateLocalizedTextAsync(request.LocalizedText);
            return result ? Result.Success() : Result.Failure("Failed to add/update localized text.");
        }
    }
}