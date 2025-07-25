using Identity.Application.Features.User.Commands.ChangePassword;
using MediatR;
using Identity.Application.Services.Interfaces;
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
            var user = await this.identityService.GetUserByIdAsync(request.UserId);

            if (user is null)
                return Result.Failure("UserId is invalid");

            await this.identityService.ChangePasswordAsync(user, request);
            loggerService.LogInfo("Change password successfully for {0}", request.UserId);

            return Result.Success();
        }
    }
}
