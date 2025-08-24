using Address.Domain.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAllAddressesByOwnerQuery : IRequest<Result>
    {
        public int OwnerId { get; set; }
        public AddressOwnerType OwnerType { get; set; }
    }

}