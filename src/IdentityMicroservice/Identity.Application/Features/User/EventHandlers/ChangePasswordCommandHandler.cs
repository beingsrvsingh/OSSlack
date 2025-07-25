using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityService identityService;
        private readonly ILoggerService<ChangePasswordCommandHandler> loggerService;

        public ChangePasswordCommandHandler(ILoggerService<ChangePasswordCommandHandler> loggerService, IIdentityService identityService)
        {
            this.identityService = identityService;
            this.loggerService = loggerService;
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.UserId);
            if (user is null)
            {
                loggerService.LogWarning("Failed to change password: User with ID {UserId} not found.", request.UserId);
                return Result.Failure(new FailureResponse(
                "UserNotFound",
                $"User with ID {request.UserId} was not found."));
            }

            var changePasswordResult = await identityService.ChangePasswordAsync(user, request);
            if (changePasswordResult == null || changePasswordResult.Succeeded != true)
            {
                var errorDetails = changePasswordResult?.Errors != null
                ? string.Join("; ", changePasswordResult.Errors)
                : "Unknown error";

                loggerService.LogWarning("Password change failed for user {UserId}. Reason(s): {Reason}", request.UserId, errorDetails);

                return Result.Failure(new FailureResponse(
                "PasswordChangeFailed",
                "An error occurred while changing the password. Please try again later."));

            }

            loggerService.LogInfo("Password changed successfully for user {UserId}.", request.UserId);
            return Result.Success("Password changed successfully.");
        }
    }
}
