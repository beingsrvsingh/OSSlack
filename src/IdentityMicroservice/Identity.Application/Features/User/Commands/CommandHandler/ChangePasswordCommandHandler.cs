using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Shared.Application.Common.Services.Interfaces;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityService identityService;
        private readonly ILoggerService loggerService;

        public ChangePasswordCommandHandler(IIdentityService identityService, ILoggerService loggerService)
        {
            this.identityService = identityService;
            this.loggerService = loggerService;
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this.identityService.GetUserByIdAsync(request.UserId);

            if (user is null)
                return Result.Failure("UserId is invalid");

            await this.identityService.ChangePasswordAsync(user, request);
            loggerService.LogInfo("Change password successfully for {0}", request.UserId);

            return Result.Success();
        }
    }
}
