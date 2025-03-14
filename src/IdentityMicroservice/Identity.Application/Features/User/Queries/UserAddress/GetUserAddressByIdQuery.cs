using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.UserAddress
{
    public class GetUserAddressByIdQuery : IRequest<Result>
    {
        public string Id { get; set; }
    }
}