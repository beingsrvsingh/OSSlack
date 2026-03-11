using Address.Domain.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAllAddressesByOwnerQuery : IRequest<Result>
    {
        public required string UserId { get; set; }
        public AddressOwnerType OwnerType { get; set; }
    }

}