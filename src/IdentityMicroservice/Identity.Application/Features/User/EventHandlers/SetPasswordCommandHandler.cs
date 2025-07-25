using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, Result>
    {
        private readonly IIdentityService identityService;

        public SetPasswordCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByEmailAsync(request.UserId);

            if (user is null)
            {
                return Result.Failure(new FailureResponse(
                    "UserNotFound",
                    $"User with ID '{request.UserId}' is invalid."));
            }

            await identityService.ResetPasswordAsync(user, request);

            return Result.Success("Password has been set successfully.");
        }
    }
}
