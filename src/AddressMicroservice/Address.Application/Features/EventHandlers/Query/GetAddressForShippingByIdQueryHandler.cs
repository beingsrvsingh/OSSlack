using Address.Application.Contracts;
using Address.Application.Features.Query;
using Address.Application.Service;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Query
{
    public class GetAddressForShippingByIdQueryHandler : IRequestHandler<GetAddressForShippingByIdQuery, Result>
    {
        private readonly IAddressService _service;

        public GetAddressForShippingByIdQueryHandler(IAddressService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(GetAddressForShippingByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _service.GetByIdAsync(request.Id);

            if (address == null)
            {
                return Result.Failure(new FailureResponse("ADDRESS_NOT_FOUND", $"No address found for Id {request.Id}"));
            }

            var dto = ShippingInfoDto.ToResponseDto(address);

            return Result.Success(dto);
        }
    }
}