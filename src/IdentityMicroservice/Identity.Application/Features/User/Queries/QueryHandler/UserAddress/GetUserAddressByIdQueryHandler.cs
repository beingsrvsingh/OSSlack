using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Application.Features.User.Queries.UserAddress;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.QueryHandler.UserAddress
{
    public class GetUserAddressByIdQueryHandler : IRequestHandler<GetUserAddressByIdQuery, Result>
    {
        private readonly IUserService userService;

        public GetUserAddressByIdQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
        {
            await userService.GetUserAddressById(request);

            return Result.Success();
        }
    }
}
