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
            var user = await this.identityService.GetUserByEmailAsync(request.Email);

            if (user is null)
            {
                return Result.Failure("Email is not valid");
            }

            var token = await this.identityService.ForgotPasswordAsync(user);

            return Result.Success(token);
        }
    }
}