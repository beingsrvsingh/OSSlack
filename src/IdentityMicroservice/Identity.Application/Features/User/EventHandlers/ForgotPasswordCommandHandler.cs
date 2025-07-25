using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IIdentityService identityService;

        public ForgotPasswordCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByEmailAsync(request.Email);

            if (user is null)
            {
                return Result.Failure(new FailureResponse(
                    "InvalidEmail",
                    $"No user found with the email '{request.Email}'."));
            }

            var token = await identityService.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(token))
            {
                return Result.Failure(new FailureResponse(
                    "TokenGenerationFailed",
                    "Failed to generate password reset token. Please try again later."));
            }

            return Result.Success(token);
        }
    }
}