using Identity.Application.Features.User.Queries.UserInfo;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.QueryHandler
{
    public class GetUserQueryHandler : IRequestHandler<GetUserInfoQuery, Result>
    {
        private readonly IIdentityService identityService;

        public GetUserQueryHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.UserId);

            if (user is null)
            {
                return Result.Failure(new FailureResponse(
                    "UserNotFound",
                    $"No records found for user ID '{request.UserId}'."));
            }

            return Result.Success(user);
        }

    }
}
