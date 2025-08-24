using Address.Application.Features.Query;
using Address.Application.Service;
using Mapster;
using MediatR;
using Shared.Utilities.Response;

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
            var entity = await _service.GetByIdAsync(request.Id);
            if (entity == null)
                return Result.Failure(new FailureResponse("NOT_FOUND", "Address not found"));

            var dto = entity.Adapt(request);
            return Result.Success(dto);
        }
    }
}