using Address.Application.Contracts;
using Address.Application.Features.Query;
using Address.Application.Service;
using Mapster;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Query
{
    public class GetAddressesByOwnerQueryHandler : IRequestHandler<GetAddressesByOwnerQuery, Result>
    {
        private readonly IAddressService _service;

        public GetAddressesByOwnerQueryHandler(IAddressService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(GetAddressesByOwnerQuery request, CancellationToken cancellationToken)
        {
            var address = await _service.GetByOwnerAsync(request.OwnerId);

            if (address == null)
            {
                return Result.Failure(new FailureResponse("ADDRESS_NOT_FOUND", $"No address found for ownerId {request.OwnerId}"));
            }

            var dto = AddressResponseDto.ToResponseDto(address);

            return Result.Success(dto);
        }


    }
}