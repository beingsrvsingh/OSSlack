using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class CreatePoojaKitCommandHandler : IRequestHandler<CreatePoojaKitCommand, Result>
    {
        private readonly ILoggerService<CreatePoojaKitCommandHandler> _logger;
        private readonly IPoojaKitService _service;

        public CreatePoojaKitCommandHandler(ILoggerService<CreatePoojaKitCommandHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(CreatePoojaKitCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.CreatePoojaKitAsync(request.PoojaKit);
            if (!success)
            {
                return Result.Failure(new FailureResponse("Error", "Failed to create pooja kit."));
            }
            return Result.Success(true);
        }
    }
}