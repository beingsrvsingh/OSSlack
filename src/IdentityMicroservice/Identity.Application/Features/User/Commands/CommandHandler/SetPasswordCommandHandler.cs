using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Identity.Application.Services.Interfaces;
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
            var user = await this.identityService.GetUserByEmailAsync(request.UserId);

            if (user is null)
                return Result.Failure("UserId is invalid");

            await this.identityService.SetPasswordAsync(user, request);

            return Result.Success();
        }
    }
}
