using Address.Domain.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAddressesByOwnerQuery : IRequest<Result>
    {
        public string UserId { get; set; }
    }

}