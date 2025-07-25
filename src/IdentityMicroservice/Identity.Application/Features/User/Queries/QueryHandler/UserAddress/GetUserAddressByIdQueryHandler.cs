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
            var userAddressDetails = await userService.GetUserAddressById(request);

            if (userAddressDetails == null)
            {
                return Result.Failure(new FailureResponse(
                    "UserAddressNotFound",
                    $"No address found for the specified ID."));
            }

            return Result.Success(userAddressDetails);
        }
    }
}
