using Address.Domain.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAddressesByOwnerQuery : IRequest<Result>
    {
        public int OwnerId { get; set; }
    }

}