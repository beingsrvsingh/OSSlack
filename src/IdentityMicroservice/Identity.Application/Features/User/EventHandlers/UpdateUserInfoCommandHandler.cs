using Identity.Application.Features.User.Commands.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler.UserInfo
{
    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand, Result>
    {
        private readonly IUserService userService;

        public UpdateUserInfoCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var hasUpdated = await userService.UpdateUserInfoAsync(request, cancellationToken);

            if (!hasUpdated)
            {
                return Result.Failure(new FailureResponse(
                    "UserInfoUpdateFailed",
                    "Failed to update user information. Please try again."));
            }

            return Result.Success("User information updated successfully.");
        }
    }
}
