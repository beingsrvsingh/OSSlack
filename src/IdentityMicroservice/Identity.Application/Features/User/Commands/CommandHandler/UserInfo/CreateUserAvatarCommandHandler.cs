using Identity.Application.Features.User.Commands.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler.UserInfo
{
    public class CreateUserAvatarCommandHandler : IRequestHandler<CreateUserAvatarCommand, Result>
    {
        private readonly IUserService userService;
        public CreateUserAvatarCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(CreateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            await userService.CreateUserAvatarAsync(request, cancellationToken);

            return Result.Success();

        }
    }
}
