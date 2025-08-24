using Address.Application.Contracts;
using Address.Application.Features.Query;
using Address.Application.Service;
using Mapster;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Query
{
    public class GetAllAddressesByOwnerQueryHandler : IRequestHandler<GetAllAddressesByOwnerQuery, Result>
    {
        private readonly IAddressService _service;

        public GetAllAddressesByOwnerQueryHandler(IAddressService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(GetAllAddressesByOwnerQuery request, CancellationToken cancellationToken)
        {
            var entities = await _service.GetAllByOwnerAsync(request.OwnerId, request.OwnerType);

            var dtos = entities.Adapt<List<AddressResponseDto>>();

            return Result.Success(dtos);
        }

    }
}