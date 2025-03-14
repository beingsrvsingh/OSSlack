using Identity.Application.Contracts;
using Identity.Application.Features.User.Queries.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.QueryHandler.UserInfo
{
    public class GetUserAvatarQueryHandler : IRequestHandler<GetUserAvatarQuery, Result>
    {
        private readonly IUserService userService;

        public GetUserAvatarQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
        {
            var userAvatar = await userService.GetUserAvatarAsync(request.Id);

            if (userAvatar is null)
                return Result.Failure(new FailureResponse("UserAvatarNotFound", new { message = "User avatar not found" }));

            return Result.Success(new UserAvatarResponse
            {
                AvatarUri = userAvatar.AvatarURI!
            });
        }
    }
}
