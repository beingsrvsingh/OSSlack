using Address.Application.Features.Commands;
using Address.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Commands
{
    public class MarkAddressAsDefaultHandler : IRequestHandler<MarkAddressAsDefaultCommand, Result>
    {
        private readonly IAddressService _service;
        private readonly ILoggerService<MarkAddressAsDefaultHandler> _logger;

        public MarkAddressAsDefaultHandler(IAddressService service, ILoggerService<MarkAddressAsDefaultHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(MarkAddressAsDefaultCommand request, CancellationToken cancellationToken)
        {
            var isMarked = await _service.MarkAddressAsDefaultAsync(request.AddressId);
            return isMarked
                ? Result.Success("Address marked successfully.")
                : Result.Failure(new FailureResponse("ADDR_MARKED_FAILED", "Address marked failed."));
        }
    }
}