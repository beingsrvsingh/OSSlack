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
            var user = await identityService.GetUserByIdAsync(request.UserId);
            if (user is null)
            {
                return Result.Failure(new FailureResponse(
                    "UserNotFound",
                    $"User with ID '{request.UserId}' is invalid."));
            }

            var resetResult = await identityService.ResetPasswordAsync(user, request, cancellationToken);

            if (!resetResult.Succeeded)
            {
                var errors = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                return Result.Failure(new FailureResponse(
                    "PasswordResetFailed",
                    $"Failed to reset password: {errors}"));
            }

            return Result.Success("Password has been set successfully.");
        }
    }
}
