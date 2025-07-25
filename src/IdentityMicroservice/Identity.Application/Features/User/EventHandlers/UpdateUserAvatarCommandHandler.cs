using Identity.Application.Features.User.Commands.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler.UserInfo
{
    public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, Result>
    {
        private readonly IUserService userService;
        public UpdateUserAvatarCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var hasUpdated = await userService.UpdateUserAvatarAsync(request, cancellationToken);

            if (!hasUpdated)
            {
                return Result.Failure(new FailureResponse(
                    "AvatarUpdateFailed",
                    "Failed to update the user avatar. Please try again."));
            }

            return Result.Success("User avatar updated successfully.");
        }
    }
}
