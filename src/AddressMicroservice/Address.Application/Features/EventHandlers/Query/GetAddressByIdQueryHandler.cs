using Address.Application.Contracts;
using Address.Application.Features.Query;
using Address.Application.Service;
using Mapster;
using MediatR;
using Shared.Utilities.Response;
using System.Net;

namespace Address.Application.Features.EventHandlers.Query
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Result>
    {
        private readonly IAddressService _service;

        public GetAddressByIdQueryHandler(IAddressService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _service.GetByIdAsync(request.Id);
            if (address == null)
                return Result.Failure(new FailureResponse("NOT_FOUND", "Address not found"));

            var dto = AddressResponseDto.ToResponseDto(address);
            return Result.Success(dto);
        }
    }
}