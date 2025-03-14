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
            await userService.UpdateUserAvatarAsync(request, cancellationToken);

            return Result.Success();

        }
    }
}
