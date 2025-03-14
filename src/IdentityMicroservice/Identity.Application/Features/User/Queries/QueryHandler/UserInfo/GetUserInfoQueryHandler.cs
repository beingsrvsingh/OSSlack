using Identity.Application.Contracts;
using Identity.Application.Features.User.Queries.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.QueryHandler.UserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Result>
    {
        private readonly IUserService userService;

        public GetUserInfoQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await userService.GetUserInfoAsync(request.Id);

            if (userInfo is null)
                return Result.Failure(new FailureResponse("UserInfoNotFound", new { mesage = "User info not found" }));

            return Result.Success(new UserInfoResponse
            {
                FirstName = userInfo.FirstName!,
                LastName = userInfo.LastName!,
            });
        }
    }
}
