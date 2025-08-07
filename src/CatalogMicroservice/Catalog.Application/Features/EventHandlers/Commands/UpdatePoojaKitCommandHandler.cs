using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class UpdatePoojaKitCommandHandler : IRequestHandler<UpdatePoojaKitCommand, Result>
    {
        private readonly ILoggerService<UpdatePoojaKitCommandHandler> _logger;
        private readonly IPoojaKitService _service;

        public UpdatePoojaKitCommandHandler(ILoggerService<UpdatePoojaKitCommandHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(UpdatePoojaKitCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.UpdatePoojaKitAsync(request.PoojaKit);
            if (!success)
            {
                return Result.Failure(new FailureResponse("Error", "Failed to update pooja kit."));
            }
            return Result.Success(true);
        }
    }
}