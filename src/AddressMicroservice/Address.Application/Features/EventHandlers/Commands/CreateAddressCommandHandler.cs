using Address.Application.Features.Commands;
using Address.Application.Service;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Commands
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result>
    {
        private readonly IAddressService _addressService;
        private readonly ILoggerService<CreateAddressCommandHandler> _logger;

        public CreateAddressCommandHandler(
            ILoggerService<CreateAddressCommandHandler> logger,
            IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        public async Task<Result> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var created = await _addressService.CreateAsync(request);

                return Result.Success(new
                {
                    Message = "Address created successfully.",
                    AddressId = created.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Address creation failed for request {@Request}", request);
                return Result.Failure(new FailureResponse("ADDRESS_CREATION_FAILED", "Failed to create address."));
            }
        }
    }
}