using Identity.Application.Contracts;
using Identity.Application.Features.User.Queries.UserInfo;
using MediatR;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;
using Mapster;

namespace Identity.Application.Features.User.Queries.QueryHandler.UserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Result>
    {
        private readonly IIdentityService identityService;
        private readonly ILoggerService<GetUserInfoQueryHandler> _logger;

        public GetUserInfoQueryHandler(ILoggerService<GetUserInfoQueryHandler> logger, IIdentityService identityService)
        {
            this._logger = logger;
            this.identityService = identityService;
        }
        public async Task<Result> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await identityService.GetUserByIdAsync(request.UserId);

            if (userInfo is null)
                return Result.Failure(new FailureResponse("UserInfoNotFound", new { mesage = "User info not found" }));

            var user = userInfo.Adapt<UserInfoResponse>();

            return Result.Success(user);
        }
    }
}
