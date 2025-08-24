using Address.Application.Contracts;
using Address.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Commands
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
    {
        private readonly IAddressService _service;
        private readonly ILoggerService<DeleteAddressCommandHandler> _logger;

        public DeleteAddressCommandHandler(IAddressService service, ILoggerService<DeleteAddressCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _service.DeleteAsync(request.Id);
            return deleted
                ? Result.Success("Address deleted successfully.")
                : Result.Failure(new FailureResponse("ADDR_DELETE_FAILED", "Address deletion failed."));
        }
    }
}