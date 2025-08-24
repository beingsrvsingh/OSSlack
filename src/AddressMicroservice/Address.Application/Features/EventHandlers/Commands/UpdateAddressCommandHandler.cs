using Address.Application.Contracts;
using Address.Application.Service;
using Address.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Commands
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Result>
    {
        private readonly IAddressService _service;
        private readonly ILoggerService<UpdateAddressCommandHandler> _logger;

        public UpdateAddressCommandHandler(IAddressService service, ILoggerService<UpdateAddressCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var updatedEntity = request.Adapt<AddressEntity>();

            var updated = await _service.UpdateAsync(request.Id, updatedEntity);
            if (updated == null)
            {
                return Result.Failure(new FailureResponse("ADDR_UPDATE_FAILED", "Address update failed"));
            }

            var dto = updated.Adapt(request);
            return Result.Success(dto);
        }
    }
}