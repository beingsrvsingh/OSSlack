using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAddressForShippingByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }
}